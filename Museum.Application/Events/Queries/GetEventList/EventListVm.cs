using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Application.MuseumServices.Queries.GetMuseumServiceList
{
    public class EventListVm
    {
        public IList<EventLookupDto> MuseumServices { get; set; }
    }
}
