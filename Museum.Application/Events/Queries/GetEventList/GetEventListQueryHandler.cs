using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Museum.Application.Interfases;
using Microsoft.EntityFrameworkCore;

namespace Museum.Application.MuseumServices.Queries.GetMuseumServiceList
{
    public class GetEventListQueryHandler
        : IRequestHandler<GetEventListQuery, EventListVm>
    {

        private readonly IMuseumDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetEventListQueryHandler(IMuseumDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<EventListVm> Handle(
            GetEventListQuery request,
            CancellationToken cancellationToken)
        {
            var museumServicesQuery = await _dbContext.Events
                .ProjectTo<EventLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new EventListVm { Events = museumServicesQuery };
        }
    }
}
