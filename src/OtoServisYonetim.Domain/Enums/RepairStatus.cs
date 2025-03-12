namespace OtoServisYonetim.Domain.Enums;

/// <summary>
/// Tamir durumlarını tanımlar
/// </summary>
public enum RepairStatus
{
    /// <summary>
    /// Tamir beklemede
    /// </summary>
    Beklemede = 1,
    
    /// <summary>
    /// Tamire başlandı
    /// </summary>
    Baslandi = 2,
    
    /// <summary>
    /// Tamir tamamlandı
    /// </summary>
    Tamamlandi = 3,
    
    /// <summary>
    /// Tamir iptal edildi
    /// </summary>
    Iptal = 4,
    
    /// <summary>
    /// Parça bekliyor
    /// </summary>
    ParcaBekliyor = 5,
    
    /// <summary>
    /// Müşteri onayı bekliyor
    /// </summary>
    MusteriOnayiBekliyor = 6,
    
    /// <summary>
    /// Test aşamasında
    /// </summary>
    TestAsamasinda = 7
}