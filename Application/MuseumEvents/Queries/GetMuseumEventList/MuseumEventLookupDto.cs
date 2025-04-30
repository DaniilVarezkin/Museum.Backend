using AutoMapper;
using Museum.Application.Common.Mapping;
using Museum.Application.MuseumEvents.Common;
using Museum.Application.MuseumEvents.Queries.GetMuseumEventDetails;
using Museum.Domain;

namespace Museum.Application.MuseumEvents.Queries.GetMuseumEventList
{
    public class MuseumEventLookupDto : IMapWith<MuseumEvent>
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Annotation { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public AudienceType AudienceType { get; set; }
        public MuseumEventType EventType { get; set; }
        public ICollection<EventPhotoDto> Photos { get; set; } = new List<EventPhotoDto>();

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<MuseumEvent, MuseumEventLookupDto>()
                .ForMember(eventDto => eventDto.Id, opt => 
                    opt.MapFrom(museumEvent => museumEvent.Id))
                .ForMember(eventDto => eventDto.Name, opt =>
                    opt.MapFrom(museumEvent => museumEvent.Name))
                .ForMember(eventDto => eventDto.Annotation, opt =>
                    opt.MapFrom(museumEvent => museumEvent.Annotation))
                .ForMember(eventDto => eventDto.AudienceType, opt =>
                    opt.MapFrom(museumEvent => museumEvent.AudienceType))
                .ForMember(eventDto => eventDto.EventType, opt =>
                    opt.MapFrom(museumEvent => museumEvent.EventType))
                .ForMember(eventDto => eventDto.StartDate, opt =>
                    opt.MapFrom(museumEvent => museumEvent.StartDate))
                .ForMember(eventDto => eventDto.EndDate, opt =>
                    opt.MapFrom(museumEvent => museumEvent.EndDate))
                .ForMember(eventDto => eventDto.Photos, opt =>
                    opt.MapFrom(museumEvent => museumEvent.Photos));
        }
    }
}
