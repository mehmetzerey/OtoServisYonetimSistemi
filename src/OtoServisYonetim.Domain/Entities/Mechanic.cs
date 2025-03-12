using OtoServisYonetim.Domain.Common;
using OtoServisYonetim.Domain.ValueObjects;

namespace OtoServisYonetim.Domain.Entities;

/// <summary>
/// Teknisyen entity'si
/// </summary>
public class Mechanic : BaseEntity
{
    /// <summary>
    /// Teknisyen adı
    /// </summary>
    public string FirstName { get; private set; } = string.Empty;
    
    /// <summary>
    /// Teknisyen soyadı
    /// </summary>
    public string LastName { get; private set; } = string.Empty;
    
    /// <summary>
    /// Teknisyen e-posta adresi
    /// </summary>
    public string Email { get; private set; } = string.Empty;
    
    /// <summary>
    /// Teknisyen telefon numarası
    /// </summary>
    public PhoneNumber PhoneNumber { get; private set; } = null!;
    
    /// <summary>
    /// Teknisyen uzmanlık alanları
    /// </summary>
    public List<string> Specializations { get; private set; } = new List<string>();
    
    /// <summary>
    /// İşe başlama tarihi
    /// </summary>
    public DateTime HireDate { get; private set; }
    
    /// <summary>
    /// Teknisyenin atandığı tamir kayıtları
    /// </summary>
    public ICollection<RepairIssue> AssignedRepairs { get; private set; } = new List<RepairIssue>();

    // EF Core için boş constructor
    private Mechanic() { }

    public Mechanic(
        string firstName,
        string lastName,
        string email,
        PhoneNumber phoneNumber,
        List<string> specializations,
        DateTime hireDate)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("Teknisyen adı boş olamaz", nameof(firstName));
        
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Teknisyen soyadı boş olamaz", nameof(lastName));
        
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("E-posta adresi boş olamaz", nameof(email));
        
        if (specializations == null || !specializations.Any())
            throw new ArgumentException("En az bir uzmanlık alanı belirtilmelidir", nameof(specializations));
        
        if (hireDate > DateTime.Now)
            throw new ArgumentException("İşe başlama tarihi gelecekte olamaz", nameof(hireDate));

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Specializations = specializations;
        HireDate = hireDate;
    }

    /// <summary>
    /// Teknisyen bilgilerini günceller
    /// </summary>
    public void UpdateContactInfo(
        string firstName,
        string lastName,
        string email,
        PhoneNumber phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("Teknisyen adı boş olamaz", nameof(firstName));
        
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Teknisyen soyadı boş olamaz", nameof(lastName));
        
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("E-posta adresi boş olamaz", nameof(email));

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    /// <summary>
    /// Teknisyen uzmanlık alanlarını günceller
    /// </summary>
    public void UpdateSpecializations(List<string> specializations)
    {
        if (specializations == null || !specializations.Any())
            throw new ArgumentException("En az bir uzmanlık alanı belirtilmelidir", nameof(specializations));

        Specializations = specializations;
    }

    /// <summary>
    /// Teknisyene uzmanlık alanı ekler
    /// </summary>
    public void AddSpecialization(string specialization)
    {
        if (string.IsNullOrWhiteSpace(specialization))
            throw new ArgumentException("Uzmanlık alanı boş olamaz", nameof(specialization));

        if (!Specializations.Contains(specialization))
        {
            Specializations.Add(specialization);
        }
    }

    /// <summary>
    /// Teknisyenden uzmanlık alanı çıkarır
    /// </summary>
    public void RemoveSpecialization(string specialization)
    {
        if (string.IsNullOrWhiteSpace(specialization))
            throw new ArgumentException("Uzmanlık alanı boş olamaz", nameof(specialization));

        if (Specializations.Count <= 1)
            throw new InvalidOperationException("Teknisyenin en az bir uzmanlık alanı olmalıdır");

        Specializations.Remove(specialization);
    }

    /// <summary>
    /// Teknisyene tamir kaydı atar
    /// </summary>
    public void AssignRepair(RepairIssue repairIssue)
    {
        if (repairIssue == null)
            throw new ArgumentNullException(nameof(repairIssue));

        AssignedRepairs.Add(repairIssue);
    }

    /// <summary>
    /// Teknisyenin tam adını döndürür
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";
}