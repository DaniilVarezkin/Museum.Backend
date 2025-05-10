using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Application.SQRS.Souvenirs.Queries.GetSouvenirList
{
    public class GetSouvenirListQuery : IRequest<SouvenirListVm>
    {
    }
}
