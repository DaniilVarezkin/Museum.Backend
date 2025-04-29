using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Application.MuseumEvents.Common
{
    public class EventPhotoDto
    {
        public Guid Id { get; set; }
        public required string FilePath {  get; set; }
    }
}
