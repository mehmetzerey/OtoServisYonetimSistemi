namespace OtoServisYonetim.Domain.Common;

/// <summary>
/// Domain event'ler için temel arayüz
/// </summary>
public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}