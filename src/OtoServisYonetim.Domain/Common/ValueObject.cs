namespace OtoServisYonetim.Domain.Common;

/// <summary>
/// Value Object'ler için temel sınıf
/// </summary>
public abstract class ValueObject
{
    /// <summary>
    /// Value Object'in eşitlik karşılaştırması için kullanılacak özellikleri döndürür
    /// </summary>
    /// <returns>Eşitlik karşılaştırması için kullanılacak özellikler</returns>
    protected abstract IEnumerable<object> GetEqualityComponents();

    /// <summary>
    /// İki Value Object'in eşit olup olmadığını kontrol eder
    /// </summary>
    /// <param name="obj">Karşılaştırılacak nesne</param>
    /// <returns>Eşitlik durumu</returns>
    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    /// <summary>
    /// Value Object için hash kodu üretir
    /// </summary>
    /// <returns>Hash kodu</returns>
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }

    /// <summary>
    /// İki Value Object'in eşit olup olmadığını kontrol eder
    /// </summary>
    /// <param name="left">Sol taraftaki Value Object</param>
    /// <param name="right">Sağ taraftaki Value Object</param>
    /// <returns>Eşitlik durumu</returns>
    public static bool operator ==(ValueObject left, ValueObject right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    /// <summary>
    /// İki Value Object'in eşit olmadığını kontrol eder
    /// </summary>
    /// <param name="left">Sol taraftaki Value Object</param>
    /// <param name="right">Sağ taraftaki Value Object</param>
    /// <returns>Eşitsizlik durumu</returns>
    public static bool operator !=(ValueObject left, ValueObject right)
    {
        return !(left == right);
    }
}