using AutoMapper;
using Museum.Application.Common.Mappings;
using Museum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Application.MuseumServices.Queries.GetMuseumServiceList
{
    public class EventLookupDto : IMapWith<Event>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Event, EventLookupDto>()
                .ForMember(dto => dto.Id, 
                    opt => opt.MapFrom(ms => ms.Id))
                .ForMember(dto => dto.Title,
                    opt => opt.MapFrom(ms => ms.Title));
        }
    }
}
