using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OtoServisYonetim.Application.Common.Interfaces;

namespace OtoServisYonetim.Application.Mechanics.Queries.GetMechanicsList
{
    /// <summary>
    /// Teknisyen listesi getirme sorgusu işleyicisi
    /// </summary>
    public class GetMechanicsListQueryHandler : IRequestHandler<GetMechanicsListQuery, MechanicsListVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// GetMechanicsListQueryHandler constructor
        /// </summary>
        /// <param name="context">Veritabanı bağlamı</param>
        /// <param name="mapper">AutoMapper</param>
        public GetMechanicsListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Sorguyu işler
        /// </summary>
        /// <param name="request">Sorgu</param>
        /// <param name="cancellationToken">İptal token</param>
        /// <returns>Teknisyen listesi görüntüleme modeli</returns>
        public async Task<MechanicsListVm> Handle(GetMechanicsListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Mechanics.AsQueryable();

            // Filtreleme
            if (request.OnlyActive.HasValue && request.OnlyActive.Value)
            {
                query = query.Where(m => m.IsActive);
            }

            if (!string.IsNullOrEmpty(request.Specialization))
            {
                query = query.Where(m => m.Specialization.Contains(request.Specialization));
            }

            if (request.MinYearsOfExperience.HasValue)
            {
                query = query.Where(m => m.YearsOfExperience >= request.MinYearsOfExperience.Value);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var mechanics = await query
                .AsNoTracking()
                .OrderBy(m => m.LastName)
                .ThenBy(m => m.FirstName)
                .ProjectTo<MechanicDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new MechanicsListVm
            {
                Mechanics = mechanics,
                TotalCount = totalCount
            };

            return vm;
        }
    }
}