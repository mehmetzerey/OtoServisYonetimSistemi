using OtoServisYonetim.Application.Common.Mappings;
using OtoServisYonetim.Domain.Entities;

namespace OtoServisYonetim.Application.Mechanics.Queries.GetMechanicsList
{
    /// <summary>
    /// Teknisyen veri transfer nesnesi
    /// </summary>
    public class MechanicDto : IMapFrom<Mechanic>
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
        /// Teknisyen uzmanlık alanı
        /// </summary>
        public string Specialization { get; set; } = string.Empty;

        /// <summary>
        /// Teknisyen deneyim yılı
        /// </summary>
        public int YearsOfExperience { get; set; }

        /// <summary>
        /// Teknisyen aktif mi?
        /// </summary>
        public bool IsActive { get; set; }
    }
}