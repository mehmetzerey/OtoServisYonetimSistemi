using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OtoServisYonetim.Application.Common.Exceptions;
using OtoServisYonetim.Application.Common.Interfaces;
using OtoServisYonetim.Domain.Entities;

namespace OtoServisYonetim.Application.Mechanics.Queries.GetMechanicById
{
    /// <summary>
    /// ID'ye göre teknisyen getirme sorgusu işleyicisi
    /// </summary>
    public class GetMechanicByIdQueryHandler : IRequestHandler<GetMechanicByIdQuery, MechanicVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// GetMechanicByIdQueryHandler constructor
        /// </summary>
        /// <param name="context">Veritabanı bağlamı</param>
        /// <param name="mapper">AutoMapper</param>
        public GetMechanicByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Sorguyu işler
        /// </summary>
        /// <param name="request">Sorgu</param>
        /// <param name="cancellationToken">İptal token</param>
        /// <returns>Teknisyen görüntüleme modeli</returns>
        public async Task<MechanicVm> Handle(GetMechanicByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Mechanics
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Mechanic), request.Id);
            }

            return _mapper.Map<MechanicVm>(entity);
        }
    }
}