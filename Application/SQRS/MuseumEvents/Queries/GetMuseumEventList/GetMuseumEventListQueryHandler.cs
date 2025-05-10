using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Museum.Application.Interfaces;

namespace Museum.Application.SQRS.MuseumEvents.Queries.GetMuseumEventList
{
    public class GetMuseumEventListQueryHandler 
        : IRequestHandler<GetMuseumEventListQuery, MuseumEventListVm>
    {
        private readonly IMuseumDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetMuseumEventListQueryHandler(IMuseumDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<MuseumEventListVm> Handle(
            GetMuseumEventListQuery query, CancellationToken cancellationToken)
        {
            var museumEventsQuery = await _dbContext.Events
                .ProjectTo<MuseumEventLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new MuseumEventListVm { MuseumEvents = museumEventsQuery };
        }
    }
}
