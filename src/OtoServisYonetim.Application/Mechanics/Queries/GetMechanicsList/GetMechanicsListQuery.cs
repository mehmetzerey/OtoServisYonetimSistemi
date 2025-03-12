using MediatR;

namespace OtoServisYonetim.Application.Mechanics.Queries.GetMechanicsList
{
    /// <summary>
    /// Teknisyen listesi getirme sorgusu
    /// </summary>
    public class GetMechanicsListQuery : IRequest<MechanicsListVm>
    {
        /// <summary>
        /// Sadece aktif teknisyenleri getir
        /// </summary>
        public bool? OnlyActive { get; set; }

        /// <summary>
        /// Uzmanlık alanına göre filtrele
        /// </summary>
        public string? Specialization { get; set; }

        /// <summary>
        /// Minimum deneyim yılı
        /// </summary>
        public int? MinYearsOfExperience { get; set; }
    }
}