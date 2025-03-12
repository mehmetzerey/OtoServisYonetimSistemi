using OtoServisYonetim.Application.Common.Mappings;
using OtoServisYonetim.Domain.Entities;
using OtoServisYonetim.Domain.ValueObjects;

namespace OtoServisYonetim.Application.Mechanics.Queries.GetMechanicById
{
    /// <summary>
    /// Teknisyen görüntüleme modeli
    /// </summary>
    public class MechanicVm : IMapFrom<Mechanic>
    {
        /// <summary>
        /// Teknisyen ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Teknisyen adı
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Teknisyen soyadı
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Teknisyen tam adı
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// Teknisyen e-posta adresi
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Teknisyen telefon numarası
        /// </summary>
        public PhoneNumber? PhoneNumber { get; set; }

        /// <summary>
        /// Teknisyen uzmanlık alanı
        /// </summary>
        public string Specialization { get; set; } = string.Empty;

        /// <summary>
        /// Teknisyen deneyim yılı
        /// </summary>
        public int YearsOfExperience { get; set; }

        /// <summary>
        /// Teknisyen sertifikaları
        /// </summary>
        public List<string> Certifications { get; set; } = new List<string>();

        /// <summary>
        /// Teknisyen aktif mi?
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Teknisyen işe başlama tarihi
        /// </summary>
        public DateTime HireDate { get; set; }
    }
}