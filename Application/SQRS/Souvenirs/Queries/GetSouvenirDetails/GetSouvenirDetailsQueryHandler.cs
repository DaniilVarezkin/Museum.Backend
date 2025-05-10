using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Museum.Application.Interfaces;
using Museum.Application.Common.Exceptions;
using Museum.Domain;

namespace Museum.Application.SQRS.Souvenirs.Queries.GetSouvenirDetails
{
    public class GetSouvenirDetailsQueryHandler
        : IRequestHandler<GetSouvenirDetailsQuery, SouvenirDetailsVm>
    {
        private readonly IMuseumDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetSouvenirDetailsQueryHandler(IMuseumDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<SouvenirDetailsVm> Handle(GetSouvenirDetailsQuery query, CancellationToken cancellationToken)
        {
            var souvenir = await _dbContext.Souvenirs.Include(s => s.Photo)
                .FirstOrDefaultAsync(s => s.Id == query.Id, cancellationToken);

            if (souvenir == null)
            {
                throw new NotFoundException(nameof(Souvenir), query.Id);
            }

            return _mapper.Map<SouvenirDetailsVm>(souvenir);
        }
    }
}
