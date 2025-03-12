using OtoServisYonetim.Domain.Common;
using OtoServisYonetim.Domain.Enums;
using OtoServisYonetim.Domain.ValueObjects;

namespace OtoServisYonetim.Domain.Entities;

/// <summary>
/// Müşteri entity'si
/// </summary>
public class Customer : BaseEntity
{
    /// <summary>
    /// Müşteri adı
    /// </summary>
    public string FirstName { get; private set; } = string.Empty;
    
    /// <summary>
    /// Müşteri soyadı
    /// </summary>
    public string LastName { get; private set; } = string.Empty;
    
    /// <summary>
    /// Müşteri e-posta adresi
    /// </summary>
    public string Email { get; private set; } = string.Empty;
    
    /// <summary>
    /// Müşteri telefon numarası
    /// </summary>
    public PhoneNumber PhoneNumber { get; private set; } = null!;
    
    /// <summary>
    /// Müşteri adresi
    /// </summary>
    public Address Address { get; private set; } = null!;
    
    /// <summary>
    /// Müşteri tipi (Bireysel/Kurumsal)
    /// </summary>
    public CustomerType CustomerType { get; private set; }
    
    /// <summary>
    /// Kurumsal müşteri için şirket adı
    /// </summary>
    public string? CompanyName { get; private set; }
    
    /// <summary>
    /// Kurumsal müşteri için vergi numarası
    /// </summary>
    public string? TaxNumber { get; private set; }
    
    /// <summary>
    /// Müşteriye ait araçlar
    /// </summary>
    public ICollection<Vehicle> Vehicles { get; private set; } = new List<Vehicle>();

    // EF Core için boş constructor
    private Customer() { }

    /// <summary>
    /// Bireysel müşteri oluşturur
    /// </summary>
    public Customer(
        string firstName,
        string lastName,
        string email,
        PhoneNumber phoneNumber,
        Address address)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("Müşteri adı boş olamaz", nameof(firstName));
        
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Müşteri soyadı boş olamaz", nameof(lastName));
        
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("E-posta adresi boş olamaz", nameof(email));

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
        CustomerType = CustomerType.Bireysel;
    }

    /// <summary>
    /// Kurumsal müşteri oluşturur
    /// </summary>
    public static Customer CreateCorporate(
        string companyName,
        string taxNumber,
        string email,
        PhoneNumber phoneNumber,
        Address address,
        string contactFirstName,
        string contactLastName)
    {
        if (string.IsNullOrWhiteSpace(companyName))
            throw new ArgumentException("Şirket adı boş olamaz", nameof(companyName));
        
        if (string.IsNullOrWhiteSpace(taxNumber))
            throw new ArgumentException("Vergi numarası boş olamaz", nameof(taxNumber));

        var customer = new Customer(contactFirstName, contactLastName, email, phoneNumber, address)
        {
            CustomerType = CustomerType.Kurumsal,
            CompanyName = companyName,
            TaxNumber = taxNumber
        };

        return customer;
    }

    /// <summary>
    /// Müşteri bilgilerini günceller
    /// </summary>
    public void UpdateContactInfo(
        string firstName,
        string lastName,
        string email,
        PhoneNumber phoneNumber,
        Address address)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("Müşteri adı boş olamaz", nameof(firstName));
        
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Müşteri soyadı boş olamaz", nameof(lastName));
        
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("E-posta adresi boş olamaz", nameof(email));

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
    }

    /// <summary>
    /// Kurumsal müşteri bilgilerini günceller
    /// </summary>
    public void UpdateCorporateInfo(string companyName, string taxNumber)
    {
        if (CustomerType != CustomerType.Kurumsal)
            throw new InvalidOperationException("Bu işlem sadece kurumsal müşteriler için geçerlidir");
            
        if (string.IsNullOrWhiteSpace(companyName))
            throw new ArgumentException("Şirket adı boş olamaz", nameof(companyName));
        
        if (string.IsNullOrWhiteSpace(taxNumber))
            throw new ArgumentException("Vergi numarası boş olamaz", nameof(taxNumber));

        CompanyName = companyName;
        TaxNumber = taxNumber;
    }

    /// <summary>
    /// Müşteriye araç ekler
    /// </summary>
    public void AddVehicle(Vehicle vehicle)
    {
        if (vehicle == null)
            throw new ArgumentNullException(nameof(vehicle));

        Vehicles.Add(vehicle);
    }

    /// <summary>
    /// Müşteriden araç çıkarır
    /// </summary>
    public void RemoveVehicle(Vehicle vehicle)
    {
        if (vehicle == null)
            throw new ArgumentNullException(nameof(vehicle));

        Vehicles.Remove(vehicle);
    }

    /// <summary>
    /// Müşterinin tam adını döndürür
    /// </summary>
    public string FullName => CustomerType == CustomerType.Kurumsal && !string.IsNullOrEmpty(CompanyName)
        ? CompanyName
        : $"{FirstName} {LastName}";
}