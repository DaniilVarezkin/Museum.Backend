using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Application.MuseumServices.Commands.DeleteMuseumService
{
    public class DeleteMuseumServiceCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
