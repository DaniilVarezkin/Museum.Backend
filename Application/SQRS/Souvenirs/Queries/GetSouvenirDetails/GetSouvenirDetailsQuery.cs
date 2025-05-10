using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Application.SQRS.Souvenirs.Queries.GetSouvenirDetails
{
    public class GetSouvenirDetailsQuery : IRequest<SouvenirDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
