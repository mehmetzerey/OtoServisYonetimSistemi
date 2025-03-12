namespace OtoServisYonetim.Domain.Common;

/// <summary>
/// Domain event'leri destekleyen entity'ler için temel sınıf
/// </summary>
public abstract class BaseAggregateRoot : BaseEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    /// <summary>
    /// Domain event'lerin listesi
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Yeni bir domain event ekler
    /// </summary>
    /// <param name="domainEvent">Eklenecek domain event</param>
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    /// <summary>
    /// Tüm domain event'leri temizler
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}