using OtoServisYonetim.Domain.Common;

namespace OtoServisYonetim.Domain.ValueObjects;

/// <summary>
/// Para birimi ve tutarını içeren value object
/// </summary>
public class Money : ValueObject
{
    /// <summary>
    /// Para tutarı
    /// </summary>
    public decimal Amount { get; private set; }
    
    /// <summary>
    /// Para birimi
    /// </summary>
    public string Currency { get; private set; }

    private Money() { }

    public Money(decimal amount, string currency)
    {
        if (amount < 0)
            throw new ArgumentException("Para tutarı negatif olamaz", nameof(amount));
        
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Para birimi boş olamaz", nameof(currency));

        Amount = amount;
        Currency = currency.ToUpper();
    }

    /// <summary>
    /// Türk Lirası para birimi oluşturur
    /// </summary>
    /// <param name="amount">Para tutarı</param>
    /// <returns>TRY para birimi</returns>
    public static Money TRY(decimal amount)
    {
        return new Money(amount, "TRY");
    }

    /// <summary>
    /// Amerikan Doları para birimi oluşturur
    /// </summary>
    /// <param name="amount">Para tutarı</param>
    /// <returns>USD para birimi</returns>
    public static Money USD(decimal amount)
    {
        return new Money(amount, "USD");
    }

    /// <summary>
    /// Euro para birimi oluşturur
    /// </summary>
    /// <param name="amount">Para tutarı</param>
    /// <returns>EUR para birimi</returns>
    public static Money EUR(decimal amount)
    {
        return new Money(amount, "EUR");
    }

    /// <summary>
    /// Para tutarını artırır
    /// </summary>
    /// <param name="amount">Artırılacak tutar</param>
    /// <returns>Yeni para nesnesi</returns>
    public Money Add(decimal amount)
    {
        return new Money(Amount + amount, Currency);
    }

    /// <summary>
    /// Para tutarını azaltır
    /// </summary>
    /// <param name="amount">Azaltılacak tutar</param>
    /// <returns>Yeni para nesnesi</returns>
    public Money Subtract(decimal amount)
    {
        if (Amount < amount)
            throw new InvalidOperationException("Sonuç negatif olamaz");
            
        return new Money(Amount - amount, Currency);
    }

    /// <summary>
    /// Para tutarını çarpar
    /// </summary>
    /// <param name="multiplier">Çarpan</param>
    /// <returns>Yeni para nesnesi</returns>
    public Money Multiply(decimal multiplier)
    {
        return new Money(Amount * multiplier, Currency);
    }

    /// <summary>
    /// Value Object'in eşitlik karşılaştırması için kullanılacak özellikleri döndürür
    /// </summary>
    /// <returns>Eşitlik karşılaştırması için kullanılacak özellikler</returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public override string ToString()
    {
        return $"{Amount:N2} {Currency}";
    }
}