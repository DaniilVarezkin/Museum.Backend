using Museum.Application.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Application.SQRS.MuseumEvents.Common
{
    public class PhotoUploadDto
    {
        public required Stream Content { get; set; }
        public required string Name { get; set; }
    }
}
