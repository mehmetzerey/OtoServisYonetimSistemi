using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OtoServisYonetim.Application.Common.Interfaces;
using OtoServisYonetim.Domain.Common;
using OtoServisYonetim.Domain.Entities;

namespace OtoServisYonetim.Persistence;

/// <summary>
/// Uygulama veritabanı bağlamı
/// </summary>
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDomainEventService _domainEventService;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ICurrentUserService currentUserService,
        IDomainEventService domainEventService) : base(options)
    {
        _currentUserService = currentUserService;
        _domainEventService = domainEventService;
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Mechanic> Mechanics => Set<Mechanic>();
    public DbSet<RepairIssue> RepairIssues => Set<RepairIssue>();

    /// <summary>
    /// Değişiklikleri kaydeder ve domain event'leri yayınlar
    /// </summary>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Etkilenen satır sayısı</returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserId;
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService.UserId;
                    entry.Entity.LastModifiedAt = DateTime.UtcNow;
                    break;
                    
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.LastModifiedBy = _currentUserService.UserId;
                    entry.Entity.LastModifiedAt = DateTime.UtcNow;
                    break;
            }
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        await DispatchEvents();

        return result;
    }

    /// <summary>
    /// Model oluşturma sırasında entity konfigürasyonlarını uygular
    /// </summary>
    /// <param name="modelBuilder">Model builder</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Soft delete filtresi
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var property = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
                var falseConstant = Expression.Constant(false);
                var expression = Expression.Equal(property, falseConstant);
                var lambda = Expression.Lambda(expression, parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }

        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Domain event'leri yayınlar
    /// </summary>
    private async Task DispatchEvents()
    {
        var domainEventEntities = ChangeTracker.Entries<BaseAggregateRoot>()
            .Select(x => x.Entity)
            .Where(x => x.DomainEvents.Any())
            .ToList();

        var domainEvents = domainEventEntities
            .SelectMany(x => x.DomainEvents)
            .ToList();

        domainEventEntities.ForEach(entity => entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await _domainEventService.PublishAsync(domainEvent);
        }
    }
}