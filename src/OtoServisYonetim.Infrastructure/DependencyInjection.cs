using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OtoServisYonetim.Application.Common.Interfaces;
using OtoServisYonetim.Infrastructure.Services;
using System.Text;

namespace OtoServisYonetim.Infrastructure;

/// <summary>
/// Infrastructure katmanı için DI servislerini kaydetmek için kullanılan sınıf
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Infrastructure katmanı servislerini kaydeder
    /// </summary>
    /// <param name="services">Servis koleksiyonu</param>
    /// <param name="configuration">Konfigürasyon</param>
    /// <returns>Servis koleksiyonu</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDomainEventService, DomainEventService>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"] ?? "DefaultKeyForDevelopment1234567890123456"))
            };
        });

        return services;
    }
}