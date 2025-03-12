using Microsoft.EntityFrameworkCore;
using OtoServisYonetim.Domain.Entities;

namespace OtoServisYonetim.Application.Common.Interfaces;

/// <summary>
/// Veritabanı işlemleri için arayüz
/// </summary>
public interface IApplicationDbContext
{
    /// <summary>
    /// Müşteriler DbSet
    /// </summary>
    DbSet<Customer> Customers { get; }
    
    /// <summary>
    /// Araçlar DbSet
    /// </summary>
    DbSet<Vehicle> Vehicles { get; }
    
    /// <summary>
    /// Teknisyenler DbSet
    /// </summary>
    DbSet<Mechanic> Mechanics { get; }
    
    /// <summary>
    /// Tamir kayıtları DbSet
    /// </summary>
    DbSet<RepairIssue> RepairIssues { get; }
    
    /// <summary>
    /// Değişiklikleri kaydeder
    /// </summary>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Etkilenen satır sayısı</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}