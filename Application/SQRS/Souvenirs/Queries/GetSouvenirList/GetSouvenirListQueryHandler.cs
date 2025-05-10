using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Museum.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Application.SQRS.Souvenirs.Queries.GetSouvenirList
{
    public class GetSouvenirListQueryHandler
        : IRequestHandler<GetSouvenirListQuery, SouvenirListVm>
    {
        private readonly IMuseumDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetSouvenirListQueryHandler(IMuseumDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<SouvenirListVm> Handle(GetSouvenirListQuery query, CancellationToken cancellationToken)
        {
            var souvenirs = await _dbContext.Souvenirs
                .ProjectTo<SouvenirLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new SouvenirListVm { Souvenirs = souvenirs };
        }
    }
}
