namespace OtoServisYonetim.Application.Common.Interfaces
{
    /// <summary>
    /// Mevcut kullanıcı bilgilerini sağlayan servis arayüzü
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// Mevcut kullanıcının ID'si
        /// </summary>
        string? UserId { get; }

        /// <summary>
        /// Kullanıcının kimlik doğrulamasından geçip geçmediği
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Kullanıcının belirli bir role sahip olup olmadığını kontrol eder
        /// </summary>
        /// <param name="role">Kontrol edilecek rol</param>
        /// <returns>Kullanıcı role sahipse true, değilse false</returns>
        bool IsInRole(string role);

        /// <summary>
        /// Kullanıcının adı
        /// </summary>
        string? UserName { get; }

        /// <summary>
        /// Kullanıcının e-posta adresi
        /// </summary>
        string? Email { get; }
    }
}