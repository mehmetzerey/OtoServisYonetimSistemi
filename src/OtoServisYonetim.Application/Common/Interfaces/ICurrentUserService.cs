namespace OtoServisYonetim.Application.Common.Interfaces;

/// <summary>
/// Mevcut kullanıcı bilgilerini sağlayan arayüz
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Mevcut kullanıcının ID'si
    /// </summary>
    string? UserId { get; }
    
    /// <summary>
    /// Mevcut kullanıcının adı
    /// </summary>
    string? UserName { get; }
    
    /// <summary>
    /// Kullanıcının kimliği doğrulanmış mı
    /// </summary>
    bool IsAuthenticated { get; }
}