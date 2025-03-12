using System.Text.RegularExpressions;
using OtoServisYonetim.Domain.Common;

namespace OtoServisYonetim.Domain.ValueObjects;

/// <summary>
/// Telefon numarası bilgilerini içeren value object
/// </summary>
public class PhoneNumber : ValueObject
{
    /// <summary>
    /// Telefon numarası
    /// </summary>
    public string Number { get; private set; }

    private PhoneNumber() { }

    public PhoneNumber(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Telefon numarası boş olamaz", nameof(number));

        // Sadece rakamları al
        var digitsOnly = new Regex(@"[^\d]").Replace(number, "");

        if (digitsOnly.Length < 10)
            throw new ArgumentException("Telefon numarası en az 10 rakam içermelidir", nameof(number));

        // Türkiye telefon numarası formatı: 05XX XXX XX XX
        if (digitsOnly.Length == 10)
        {
            Number = $"0{digitsOnly}";
        }
        else if (digitsOnly.Length == 11 && digitsOnly.StartsWith("0"))
        {
            Number = digitsOnly;
        }
        else
        {
            throw new ArgumentException("Geçersiz telefon numarası formatı", nameof(number));
        }
    }

    /// <summary>
    /// Value Object'in eşitlik karşılaştırması için kullanılacak özellikleri döndürür
    /// </summary>
    /// <returns>Eşitlik karşılaştırması için kullanılacak özellikler</returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Number;
    }

    /// <summary>
    /// Telefon numarasını formatlar
    /// </summary>
    /// <returns>Formatlanmış telefon numarası</returns>
    public string Format()
    {
        if (Number.Length == 11)
        {
            return $"{Number.Substring(0, 4)} {Number.Substring(4, 3)} {Number.Substring(7, 2)} {Number.Substring(9, 2)}";
        }

        return Number;
    }

    public override string ToString()
    {
        return Format();
    }
}