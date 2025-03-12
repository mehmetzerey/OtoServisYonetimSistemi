using OtoServisYonetim.Domain.Common;

namespace OtoServisYonetim.Domain.ValueObjects;

/// <summary>
/// Adres bilgilerini içeren value object
/// </summary>
public class Address : ValueObject
{
    /// <summary>
    /// Sokak bilgisi
    /// </summary>
    public string Street { get; private set; }
    
    /// <summary>
    /// İlçe bilgisi
    /// </summary>
    public string District { get; private set; }
    
    /// <summary>
    /// İl bilgisi
    /// </summary>
    public string City { get; private set; }
    
    /// <summary>
    /// Posta kodu
    /// </summary>
    public string PostalCode { get; private set; }

    private Address() { }

    public Address(string street, string district, string city, string postalCode)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Sokak bilgisi boş olamaz", nameof(street));
        
        if (string.IsNullOrWhiteSpace(district))
            throw new ArgumentException("İlçe bilgisi boş olamaz", nameof(district));
        
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("İl bilgisi boş olamaz", nameof(city));
        
        if (string.IsNullOrWhiteSpace(postalCode))
            throw new ArgumentException("Posta kodu boş olamaz", nameof(postalCode));

        Street = street;
        District = district;
        City = city;
        PostalCode = postalCode;
    }

    /// <summary>
    /// Value Object'in eşitlik karşılaştırması için kullanılacak özellikleri döndürür
    /// </summary>
    /// <returns>Eşitlik karşılaştırması için kullanılacak özellikler</returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return District;
        yield return City;
        yield return PostalCode;
    }

    public override string ToString()
    {
        return $"{Street}, {District}, {City}, {PostalCode}";
    }
}