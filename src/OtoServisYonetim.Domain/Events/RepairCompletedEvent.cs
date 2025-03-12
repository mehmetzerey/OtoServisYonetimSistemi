using OtoServisYonetim.Domain.Common;

namespace OtoServisYonetim.Domain.Events;

/// <summary>
/// Tamir tamamlandığında tetiklenen domain event
/// </summary>
public class RepairCompletedEvent : DomainEvent
{
    /// <summary>
    /// Tamir kaydının ID'si
    /// </summary>
    public Guid RepairIssueId { get; }
    
    /// <summary>
    /// Müşteri ID'si
    /// </summary>
    public Guid CustomerId { get; }
    
    /// <summary>
    /// Araç ID'si
    /// </summary>
    public Guid VehicleId { get; }
    
    /// <summary>
    /// Teknisyen ID'si
    /// </summary>
    public Guid MechanicId { get; }
    
    /// <summary>
    /// Tamamlanma tarihi
    /// </summary>
    public DateTime CompletedDate { get; }
    
    /// <summary>
    /// Tamir notları
    /// </summary>
    public string Notes { get; }

    public RepairCompletedEvent(
        Guid repairIssueId, 
        Guid customerId, 
        Guid vehicleId, 
        Guid mechanicId, 
        DateTime completedDate, 
        string notes)
    {
        RepairIssueId = repairIssueId;
        CustomerId = customerId;
        VehicleId = vehicleId;
        MechanicId = mechanicId;
        CompletedDate = completedDate;
        Notes = notes;
    }
}