namespace OtoServisYonetim.Application.Mechanics.Queries.GetMechanicsList
{
    /// <summary>
    /// Teknisyen listesi görüntüleme modeli
    /// </summary>
    public class MechanicsListVm
    {
        /// <summary>
        /// Teknisyen listesi
        /// </summary>
        public IList<MechanicDto> Mechanics { get; set; } = new List<MechanicDto>();

        /// <summary>
        /// Toplam teknisyen sayısı
        /// </summary>
        public int TotalCount { get; set; }
    }
}