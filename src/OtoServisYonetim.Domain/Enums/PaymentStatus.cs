namespace OtoServisYonetim.Domain.Enums;

/// <summary>
/// Ödeme durumlarını tanımlar
/// </summary>
public enum PaymentStatus
{
    /// <summary>
    /// Ödeme yapılmadı
    /// </summary>
    Odenmedi = 1,
    
    /// <summary>
    /// Kısmi ödeme yapıldı
    /// </summary>
    KismiOdeme = 2,
    
    /// <summary>
    /// Ödeme tamamlandı
    /// </summary>
    Tamamlandi = 3,
    
    /// <summary>
    /// Ödeme iade edildi
    /// </summary>
    IadeEdildi = 4,
    
    /// <summary>
    /// Ödeme beklemede
    /// </summary>
    Beklemede = 5
}