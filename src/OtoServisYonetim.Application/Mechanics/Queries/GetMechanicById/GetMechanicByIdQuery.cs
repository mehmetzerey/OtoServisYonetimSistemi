using MediatR;

namespace OtoServisYonetim.Application.Mechanics.Queries.GetMechanicById
{
    /// <summary>
    /// ID'ye göre teknisyen getirme sorgusu
    /// </summary>
    public class GetMechanicByIdQuery : IRequest<MechanicVm>
    {
        /// <summary>
        /// Teknisyen ID
        /// </summary>
        public Guid Id { get; set; }
    }
}