using OtoServisYonetim.Application.Common.Interfaces;
using System.Security.Claims;

namespace OtoServisYonetim.API.Services
{
    /// <summary>
    /// Mevcut kullanıcı bilgilerini sağlayan servis
    /// </summary>
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// CurrentUserService constructor
        /// </summary>
        /// <param name="httpContextAccessor">HTTP context accessor</param>
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Mevcut kullanıcının ID'sini döndürür
        /// </summary>
        public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        /// <summary>
        /// Kullanıcının kimlik doğrulamasından geçip geçmediğini kontrol eder
        /// </summary>
        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        /// <summary>
        /// Kullanıcının belirli bir role sahip olup olmadığını kontrol eder
        /// </summary>
        /// <param name="role">Kontrol edilecek rol</param>
        /// <returns>Kullanıcı role sahipse true, değilse false</returns>
        public bool IsInRole(string role)
        {
            return _httpContextAccessor.HttpContext?.User?.IsInRole(role) ?? false;
        }

        /// <summary>
        /// Kullanıcının adını döndürür
        /// </summary>
        public string? UserName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);

        /// <summary>
        /// Kullanıcının e-posta adresini döndürür
        /// </summary>
        public string? Email => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
    }
}