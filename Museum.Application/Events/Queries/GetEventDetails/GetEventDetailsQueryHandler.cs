using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Museum.Application.Interfases;
using Museum.Application.Common.Exceptions;
using Museum.Domain;

namespace Museum.Application.MuseumServices.Queries.GetMuseumServiceDetails
{
    public class GetEventDetailsQueryHandler
        : IRequestHandler<GetEventDetailsQuery, EventeVm>
    {
        private readonly IMuseumDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetEventDetailsQueryHandler(IMuseumDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<EventeVm> Handle(
            GetEventDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Events
                .FirstOrDefaultAsync(ms =>
                    ms.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Event), request.Id);
            }

            return _mapper.Map<EventeVm>(entity);
        }
    }
}
