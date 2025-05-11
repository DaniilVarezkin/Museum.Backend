using MediatR;
using Museum.Domain;
using Microsoft.EntityFrameworkCore;
using Museum.Application.Interfaces;
using Museum.Application.Common.Exceptions;
using AutoMapper;

namespace Museum.Application.SQRS.MuseumEvents.Queries.GetMuseumEventDetails
{
    public class GetMuseumEventDetailsQueryHandler
        : IRequestHandler<GetMuseumEventDetailsQuery, MuseumEventDetailsVm>
    {
        private readonly IMuseumDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetMuseumEventDetailsQueryHandler(IMuseumDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<MuseumEventDetailsVm> Handle(
            GetMuseumEventDetailsQuery query, CancellationToken cancellationToken)
        {
            var museumEvent = await _dbContext.Events.Include(museumEvent => museumEvent.Photos)
                .FirstOrDefaultAsync(museumEvent => 
                    museumEvent.Id == query.Id,
                    cancellationToken);

            if (museumEvent == null)
            {
                throw new NotFoundException(nameof(MuseumEvent), query.Id);
            }

            return _mapper.Map<MuseumEventDetailsVm>(museumEvent);
        }
    }
}
