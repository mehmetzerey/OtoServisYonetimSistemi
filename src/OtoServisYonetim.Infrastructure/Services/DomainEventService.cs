using MediatR;
using Microsoft.Extensions.Logging;
using OtoServisYonetim.Application.Common.Interfaces;
using OtoServisYonetim.Domain.Common;

namespace OtoServisYonetim.Infrastructure.Services;

/// <summary>
/// Domain event'leri işlemek için kullanılan servis
/// </summary>
public class DomainEventService : IDomainEventService
{
    private readonly ILogger<DomainEventService> _logger;
    private readonly IPublisher _mediator;

    public DomainEventService(ILogger<DomainEventService> logger, IPublisher mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Domain event'i yayınlar
    /// </summary>
    /// <param name="domainEvent">Yayınlanacak domain event</param>
    public async Task PublishAsync(IDomainEvent domainEvent)
    {
        _logger.LogInformation("Domain Event yayınlanıyor. Event: {event}", domainEvent.GetType().Name);
        await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
    }

    /// <summary>
    /// Domain event'e karşılık gelen notification'ı döndürür
    /// </summary>
    /// <param name="domainEvent">Domain event</param>
    /// <returns>Notification</returns>
    private INotification GetNotificationCorrespondingToDomainEvent(IDomainEvent domainEvent)
    {
        return (INotification)Activator.CreateInstance(
            typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent)!;
    }
}

/// <summary>
/// Domain event notification'ı
/// </summary>
/// <typeparam name="TDomainEvent">Domain event tipi</typeparam>
public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : IDomainEvent
{
    /// <summary>
    /// Domain event
    /// </summary>
    public TDomainEvent DomainEvent { get; }

    public DomainEventNotification(TDomainEvent domainEvent)
    {
        DomainEvent = domainEvent;
    }
}