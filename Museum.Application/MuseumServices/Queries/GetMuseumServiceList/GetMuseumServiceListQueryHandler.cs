using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Museum.Application.Interfases;
using Microsoft.EntityFrameworkCore;

namespace Museum.Application.MuseumServices.Queries.GetMuseumServiceList
{
    public class GetMuseumServiceListQueryHandler
        : IRequestHandler<GetMuseumServiceListQuery, MuseumServiceListVm>
    {

        private readonly IMuseumDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetMuseumServiceListQueryHandler(IMuseumDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<MuseumServiceListVm> Handle(
            GetMuseumServiceListQuery request,
            CancellationToken cancellationToken)
        {
            var museumServicesQuery = await _dbContext.MuseumServices
                .ProjectTo<MuseumServiceLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new MuseumServiceListVm { MuseumServices = museumServicesQuery };
        }
    }
}
