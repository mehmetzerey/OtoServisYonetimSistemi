using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OtoServisYonetim.Application.Common.Interfaces;

namespace OtoServisYonetim.Persistence;

/// <summary>
/// Persistence katmanı için DI servislerini kaydetmek için kullanılan sınıf
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Persistence katmanı servislerini kaydeder
    /// </summary>
    /// <param name="services">Servis koleksiyonu</param>
    /// <param name="configuration">Konfigürasyon</param>
    /// <returns>Servis koleksiyonu</returns>
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}