using OtoServisYonetim.Domain.Common;

namespace OtoServisYonetim.Application.Common.Interfaces;

/// <summary>
/// Domain event'leri işlemek için arayüz
/// </summary>
public interface IDomainEventService
{
    /// <summary>
    /// Domain event'i yayınlar
    /// </summary>
    /// <param name="domainEvent">Yayınlanacak domain event</param>
    Task PublishAsync(IDomainEvent domainEvent);
}