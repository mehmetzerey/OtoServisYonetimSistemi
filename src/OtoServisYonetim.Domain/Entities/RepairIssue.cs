using OtoServisYonetim.Domain.Common;
using OtoServisYonetim.Domain.Enums;
using OtoServisYonetim.Domain.Events;
using OtoServisYonetim.Domain.ValueObjects;

namespace OtoServisYonetim.Domain.Entities;

/// <summary>
/// Tamir kaydı entity'si
/// </summary>
public class RepairIssue : BaseAggregateRoot
{
    /// <summary>
    /// Araç ID'si
    /// </summary>
    public Guid VehicleId { get; private set; }
    
    /// <summary>
    /// Araç
    /// </summary>
    public Vehicle? Vehicle { get; private set; }
    
    /// <summary>
    /// Müşteri ID'si
    /// </summary>
    public Guid CustomerId { get; private set; }
    
    /// <summary>
    /// Müşteri
    /// </summary>
    public Customer? Customer { get; private set; }
    
    /// <summary>
    /// Teknisyen ID'si
    /// </summary>
    public Guid? MechanicId { get; private set; }
    
    /// <summary>
    /// Teknisyen
    /// </summary>
    public Mechanic? Mechanic { get; private set; }
    
    /// <summary>
    /// Tamir başlığı
    /// </summary>
    public string Title { get; private set; } = string.Empty;
    
    /// <summary>
    /// Tamir açıklaması
    /// </summary>
    public string Description { get; private set; } = string.Empty;
    
    /// <summary>
    /// Tamir durumu
    /// </summary>
    public RepairStatus Status { get; private set; }
    
    /// <summary>
    /// Tahmini maliyet
    /// </summary>
    public Money EstimatedCost { get; private set; } = null!;
    
    /// <summary>
    /// Gerçek maliyet
    /// </summary>
    public Money? ActualCost { get; private set; }
    
    /// <summary>
    /// Ödeme durumu
    /// </summary>
    public PaymentStatus PaymentStatus { get; private set; }
    
    /// <summary>
    /// Tamir başlangıç tarihi
    /// </summary>
    public DateTime? StartDate { get; private set; }
    
    /// <summary>
    /// Tamir bitiş tarihi
    /// </summary>
    public DateTime? CompletionDate { get; private set; }
    
    /// <summary>
    /// Tamir notları
    /// </summary>
    public string Notes { get; private set; } = string.Empty;
    
    /// <summary>
    /// Kullanılan parçalar
    /// </summary>
    public List<string> UsedParts { get; private set; } = new List<string>();

    // EF Core için boş constructor
    private RepairIssue() { }

    public RepairIssue(
        Guid vehicleId,
        Guid customerId,
        string title,
        string description,
        Money estimatedCost)
    {
        if (vehicleId == Guid.Empty)
            throw new ArgumentException("Geçersiz araç ID'si", nameof(vehicleId));
        
        if (customerId == Guid.Empty)
            throw new ArgumentException("Geçersiz müşteri ID'si", nameof(customerId));
        
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Tamir başlığı boş olamaz", nameof(title));
        
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Tamir açıklaması boş olamaz", nameof(description));

        VehicleId = vehicleId;
        CustomerId = customerId;
        Title = title;
        Description = description;
        EstimatedCost = estimatedCost;
        Status = RepairStatus.Beklemede;
        PaymentStatus = PaymentStatus.Odenmedi;
    }

    /// <summary>
    /// Tamire teknisyen atar
    /// </summary>
    public void AssignMechanic(Guid mechanicId)
    {
        if (mechanicId == Guid.Empty)
            throw new ArgumentException("Geçersiz teknisyen ID'si", nameof(mechanicId));

        MechanicId = mechanicId;
    }

    /// <summary>
    /// Tamiri başlatır
    /// </summary>
    public void StartRepair()
    {
        if (Status != RepairStatus.Beklemede && Status != RepairStatus.ParcaBekliyor && Status != RepairStatus.MusteriOnayiBekliyor)
            throw new InvalidOperationException($"Tamir şu anda {Status} durumunda, başlatılamaz");

        if (MechanicId == null)
            throw new InvalidOperationException("Tamire başlamadan önce bir teknisyen atanmalıdır");

        Status = RepairStatus.Baslandi;
        StartDate = DateTime.Now;
    }

    /// <summary>
    /// Tamiri tamamlar
    /// </summary>
    public void CompleteRepair(Money actualCost, string notes)
    {
        if (Status != RepairStatus.Baslandi && Status != RepairStatus.TestAsamasinda)
            throw new InvalidOperationException($"Tamir şu anda {Status} durumunda, tamamlanamaz");

        if (MechanicId == null)
            throw new InvalidOperationException("Tamiri tamamlamadan önce bir teknisyen atanmalıdır");

        Status = RepairStatus.Tamamlandi;
        CompletionDate = DateTime.Now;
        ActualCost = actualCost;
        Notes = notes;

        // Domain event ekle
        AddDomainEvent(new RepairCompletedEvent(
            Id,
            CustomerId,
            VehicleId,
            MechanicId.Value,
            CompletionDate.Value,
            Notes));
    }

    /// <summary>
    /// Tamiri iptal eder
    /// </summary>
    public void CancelRepair(string reason)
    {
        if (Status == RepairStatus.Tamamlandi)
            throw new InvalidOperationException("Tamamlanmış bir tamir iptal edilemez");

        Status = RepairStatus.Iptal;
        Notes = $"İptal nedeni: {reason}";
    }

    /// <summary>
    /// Tamir durumunu günceller
    /// </summary>
    public void UpdateStatus(RepairStatus status)
    {
        Status = status;
    }

    /// <summary>
    /// Ödeme durumunu günceller
    /// </summary>
    public void UpdatePaymentStatus(PaymentStatus paymentStatus, Money? paidAmount = null)
    {
        PaymentStatus = paymentStatus;

        if (paymentStatus == PaymentStatus.Tamamlandi && paidAmount != null)
        {
            // Domain event ekle
            AddDomainEvent(new PaymentReceivedEvent(
                Guid.NewGuid(), // Ödeme ID'si
                Id,
                CustomerId,
                paidAmount,
                DateTime.Now,
                paymentStatus));
        }
    }

    /// <summary>
    /// Tahmini maliyeti günceller
    /// </summary>
    public void UpdateEstimatedCost(Money estimatedCost)
    {
        EstimatedCost = estimatedCost;
    }

    /// <summary>
    /// Tamir detaylarını günceller
    /// </summary>
    public void UpdateDetails(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Tamir başlığı boş olamaz", nameof(title));
        
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Tamir açıklaması boş olamaz", nameof(description));

        Title = title;
        Description = description;
    }

    /// <summary>
    /// Kullanılan parça ekler
    /// </summary>
    public void AddUsedPart(string partName)
    {
        if (string.IsNullOrWhiteSpace(partName))
            throw new ArgumentException("Parça adı boş olamaz", nameof(partName));

        UsedParts.Add(partName);
    }

    /// <summary>
    /// Notları günceller
    /// </summary>
    public void UpdateNotes(string notes)
    {
        Notes = notes;
    }
}