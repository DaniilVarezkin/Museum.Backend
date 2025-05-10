using MediatR;
using Museum.Application.SQRS.MuseumEvents.Common;
using Museum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Application.SQRS.Souvenirs.Commands
{
    public class CreateSouvenirCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public PhotoUploadDto? PhotoDto { get; set; } = null;
    }
}
