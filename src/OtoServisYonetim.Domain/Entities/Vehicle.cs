using OtoServisYonetim.Domain.Common;
using OtoServisYonetim.Domain.Enums;
using OtoServisYonetim.Domain.ValueObjects;

namespace OtoServisYonetim.Domain.Entities;

/// <summary>
/// Araç entity'si
/// </summary>
public class Vehicle : BaseEntity
{
    /// <summary>
    /// Araç sahibi müşteri ID'si
    /// </summary>
    public Guid CustomerId { get; private set; }
    
    /// <summary>
    /// Araç sahibi müşteri
    /// </summary>
    public Customer? Customer { get; private set; }
    
    /// <summary>
    /// Araç markası
    /// </summary>
    public string Brand { get; private set; } = string.Empty;
    
    /// <summary>
    /// Araç modeli
    /// </summary>
    public string Model { get; private set; } = string.Empty;
    
    /// <summary>
    /// Araç üretim yılı
    /// </summary>
    public int Year { get; private set; }
    
    /// <summary>
    /// Araç plakası
    /// </summary>
    public string LicensePlate { get; private set; } = string.Empty;
    
    /// <summary>
    /// Araç tipi
    /// </summary>
    public VehicleType VehicleType { get; private set; }
    
    /// <summary>
    /// Araç kimlik bilgileri
    /// </summary>
    public VehicleIdentification Identification { get; private set; } = null!;
    
    /// <summary>
    /// Araç kilometresi
    /// </summary>
    public int Mileage { get; private set; }
    
    /// <summary>
    /// Araç rengi
    /// </summary>
    public string Color { get; private set; } = string.Empty;
    
    /// <summary>
    /// Araç için yapılan tamir kayıtları
    /// </summary>
    public ICollection<RepairIssue> RepairIssues { get; private set; } = new List<RepairIssue>();

    // EF Core için boş constructor
    private Vehicle() { }

    public Vehicle(
        Guid customerId,
        string brand,
        string model,
        int year,
        string licensePlate,
        VehicleType vehicleType,
        VehicleIdentification identification,
        int mileage,
        string color)
    {
        if (string.IsNullOrWhiteSpace(brand))
            throw new ArgumentException("Araç markası boş olamaz", nameof(brand));
        
        if (string.IsNullOrWhiteSpace(model))
            throw new ArgumentException("Araç modeli boş olamaz", nameof(model));
        
        if (year < 1900 || year > DateTime.Now.Year)
            throw new ArgumentException("Geçersiz üretim yılı", nameof(year));
        
        if (string.IsNullOrWhiteSpace(licensePlate))
            throw new ArgumentException("Araç plakası boş olamaz", nameof(licensePlate));
        
        if (mileage < 0)
            throw new ArgumentException("Kilometre değeri negatif olamaz", nameof(mileage));
        
        if (string.IsNullOrWhiteSpace(color))
            throw new ArgumentException("Araç rengi boş olamaz", nameof(color));

        CustomerId = customerId;
        Brand = brand;
        Model = model;
        Year = year;
        LicensePlate = licensePlate.ToUpper();
        VehicleType = vehicleType;
        Identification = identification;
        Mileage = mileage;
        Color = color;
    }

    /// <summary>
    /// Araç bilgilerini günceller
    /// </summary>
    public void UpdateDetails(
        string brand,
        string model,
        int year,
        string licensePlate,
        VehicleType vehicleType,
        string color)
    {
        if (string.IsNullOrWhiteSpace(brand))
            throw new ArgumentException("Araç markası boş olamaz", nameof(brand));
        
        if (string.IsNullOrWhiteSpace(model))
            throw new ArgumentException("Araç modeli boş olamaz", nameof(model));
        
        if (year < 1900 || year > DateTime.Now.Year)
            throw new ArgumentException("Geçersiz üretim yılı", nameof(year));
        
        if (string.IsNullOrWhiteSpace(licensePlate))
            throw new ArgumentException("Araç plakası boş olamaz", nameof(licensePlate));
        
        if (string.IsNullOrWhiteSpace(color))
            throw new ArgumentException("Araç rengi boş olamaz", nameof(color));

        Brand = brand;
        Model = model;
        Year = year;
        LicensePlate = licensePlate.ToUpper();
        VehicleType = vehicleType;
        Color = color;
    }

    /// <summary>
    /// Araç kilometresini günceller
    /// </summary>
    public void UpdateMileage(int mileage)
    {
        if (mileage < Mileage)
            throw new ArgumentException("Yeni kilometre değeri mevcut değerden küçük olamaz", nameof(mileage));

        Mileage = mileage;
    }

    /// <summary>
    /// Araç kimlik bilgilerini günceller
    /// </summary>
    public void UpdateIdentification(VehicleIdentification identification)
    {
        Identification = identification ?? throw new ArgumentNullException(nameof(identification));
    }

    /// <summary>
    /// Araca tamir kaydı ekler
    /// </summary>
    public void AddRepairIssue(RepairIssue repairIssue)
    {
        if (repairIssue == null)
            throw new ArgumentNullException(nameof(repairIssue));

        RepairIssues.Add(repairIssue);
    }

    /// <summary>
    /// Araç sahibini değiştirir
    /// </summary>
    public void ChangeOwner(Guid newCustomerId)
    {
        if (newCustomerId == Guid.Empty)
            throw new ArgumentException("Geçersiz müşteri ID'si", nameof(newCustomerId));

        CustomerId = newCustomerId;
    }

    /// <summary>
    /// Araç bilgilerini döndürür
    /// </summary>
    public override string ToString()
    {
        return $"{Year} {Brand} {Model} - {LicensePlate}";
    }
}