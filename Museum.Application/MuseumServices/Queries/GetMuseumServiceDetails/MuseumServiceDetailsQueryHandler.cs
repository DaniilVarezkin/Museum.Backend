using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Museum.Application.Interfases;
using Museum.Application.Common.Exceptions;
using Museum.Domain;

namespace Museum.Application.MuseumServices.Queries.GetMuseumServiceDetails
{
    public class MuseumServiceDetailsQueryHandler
        : IRequestHandler<GetMuseumServiceDetailsQuery, MuseumServiceVm>
    {
        private readonly IMuseumDbContext _dbContext;
        private readonly IMapper _mapper;

        public MuseumServiceDetailsQueryHandler(IMuseumDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<MuseumServiceVm> Handle(
            GetMuseumServiceDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.MuseumServices
                .FirstOrDefaultAsync(ms =>
                    ms.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(MuseumService), request.Id);
            }

            return _mapper.Map<MuseumServiceVm>(entity);
        }
    }
}
