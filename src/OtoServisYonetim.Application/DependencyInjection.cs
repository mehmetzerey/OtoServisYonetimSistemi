using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OtoServisYonetim.Application.Common.Behaviors;

namespace OtoServisYonetim.Application;

/// <summary>
/// Application katmanı için DI servislerini kaydetmek için kullanılan sınıf
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Application katmanı servislerini kaydeder
    /// </summary>
    /// <param name="services">Servis koleksiyonu</param>
    /// <returns>Servis koleksiyonu</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        return services;
    }
}