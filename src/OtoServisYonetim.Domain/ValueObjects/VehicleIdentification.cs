using System.Text.RegularExpressions;
using OtoServisYonetim.Domain.Common;

namespace OtoServisYonetim.Domain.ValueObjects;

/// <summary>
/// Araç kimlik bilgilerini içeren value object
/// </summary>
public class VehicleIdentification : ValueObject
{
    /// <summary>
    /// Şasi numarası
    /// </summary>
    public string ChassisNumber { get; private set; }
    
    /// <summary>
    /// Motor numarası
    /// </summary>
    public string EngineNumber { get; private set; }

    private VehicleIdentification() { }

    public VehicleIdentification(string chassisNumber, string engineNumber)
    {
        ValidateChassisNumber(chassisNumber);
        ValidateEngineNumber(engineNumber);

        ChassisNumber = chassisNumber.ToUpper();
        EngineNumber = engineNumber.ToUpper();
    }

    /// <summary>
    /// Şasi numarasını doğrular
    /// </summary>
    /// <param name="chassisNumber">Doğrulanacak şasi numarası</param>
    /// <exception cref="ArgumentException">Geçersiz şasi numarası durumunda fırlatılır</exception>
    private void ValidateChassisNumber(string chassisNumber)
    {
        if (string.IsNullOrWhiteSpace(chassisNumber))
            throw new ArgumentException("Şasi numarası boş olamaz", nameof(chassisNumber));

        // Şasi numarası genellikle 17 karakter uzunluğundadır ve sadece harf ve rakam içerir
        if (chassisNumber.Length != 17)
            throw new ArgumentException("Şasi numarası 17 karakter uzunluğunda olmalıdır", nameof(chassisNumber));

        if (!Regex.IsMatch(chassisNumber, @"^[A-Za-z0-9]+$"))
            throw new ArgumentException("Şasi numarası sadece harf ve rakam içerebilir", nameof(chassisNumber));
    }

    /// <summary>
    /// Motor numarasını doğrular
    /// </summary>
    /// <param name="engineNumber">Doğrulanacak motor numarası</param>
    /// <exception cref="ArgumentException">Geçersiz motor numarası durumunda fırlatılır</exception>
    private void ValidateEngineNumber(string engineNumber)
    {
        if (string.IsNullOrWhiteSpace(engineNumber))
            throw new ArgumentException("Motor numarası boş olamaz", nameof(engineNumber));

        // Motor numarası genellikle 8-15 karakter arasındadır ve sadece harf ve rakam içerir
        if (engineNumber.Length < 8 || engineNumber.Length > 15)
            throw new ArgumentException("Motor numarası 8-15 karakter arasında olmalıdır", nameof(engineNumber));

        if (!Regex.IsMatch(engineNumber, @"^[A-Za-z0-9]+$"))
            throw new ArgumentException("Motor numarası sadece harf ve rakam içerebilir", nameof(engineNumber));
    }

    /// <summary>
    /// Value Object'in eşitlik karşılaştırması için kullanılacak özellikleri döndürür
    /// </summary>
    /// <returns>Eşitlik karşılaştırması için kullanılacak özellikler</returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ChassisNumber;
        yield return EngineNumber;
    }
}