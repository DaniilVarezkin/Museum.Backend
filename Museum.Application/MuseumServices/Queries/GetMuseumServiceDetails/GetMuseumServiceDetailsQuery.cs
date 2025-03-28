using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Application.MuseumServices.Queries.GetMuseumServiceDetails
{
    public class GetMuseumServiceDetailsQuery : IRequest<MuseumServiceVm>
    {
        public Guid Id { get; set; }
    }
}
