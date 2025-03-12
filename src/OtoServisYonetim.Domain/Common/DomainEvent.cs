namespace OtoServisYonetim.Domain.Common;

/// <summary>
/// Domain event'ler için temel sınıf
/// </summary>
public abstract class DomainEvent : IDomainEvent
{
    public DateTime OccurredOn { get; }

    protected DomainEvent()
    {
        OccurredOn = DateTime.UtcNow;
    }
}