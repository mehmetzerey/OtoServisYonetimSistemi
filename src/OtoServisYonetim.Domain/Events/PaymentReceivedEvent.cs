using OtoServisYonetim.Domain.Common;
using OtoServisYonetim.Domain.Enums;
using OtoServisYonetim.Domain.ValueObjects;

namespace OtoServisYonetim.Domain.Events;

/// <summary>
/// Ödeme alındığında tetiklenen domain event
/// </summary>
public class PaymentReceivedEvent : DomainEvent
{
    /// <summary>
    /// Ödeme ID'si
    /// </summary>
    public Guid PaymentId { get; }
    
    /// <summary>
    /// Tamir kaydının ID'si
    /// </summary>
    public Guid RepairIssueId { get; }
    
    /// <summary>
    /// Müşteri ID'si
    /// </summary>
    public Guid CustomerId { get; }
    
    /// <summary>
    /// Ödeme tutarı
    /// </summary>
    public Money Amount { get; }
    
    /// <summary>
    /// Ödeme tarihi
    /// </summary>
    public DateTime PaymentDate { get; }
    
    /// <summary>
    /// Ödeme durumu
    /// </summary>
    public PaymentStatus PaymentStatus { get; }

    public PaymentReceivedEvent(
        Guid paymentId, 
        Guid repairIssueId, 
        Guid customerId, 
        Money amount, 
        DateTime paymentDate, 
        PaymentStatus paymentStatus)
    {
        PaymentId = paymentId;
        RepairIssueId = repairIssueId;
        CustomerId = customerId;
        Amount = amount;
        PaymentDate = paymentDate;
        PaymentStatus = paymentStatus;
    }
}