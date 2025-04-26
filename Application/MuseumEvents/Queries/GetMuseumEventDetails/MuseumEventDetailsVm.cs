using AutoMapper;
using Museum.Application.Common.Mapping;
using Museum.Domain;

namespace Museum.Application.MuseumEvents.Queries.GetMuseumEventDetails
{
    public class MuseumEventDetailsVm : IMapWith<MuseumEvent>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Annotation { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public AudienceType AudienceType { get; set; }
        public MuseumEventType EventType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string TicketLink { get; set; } = string.Empty;
        public ICollection<EventPhoto> Photos { get; set; } = new List<EventPhoto>();

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<MuseumEvent,  MuseumEventDetailsVm>()
                .ForMember(detailsVm => detailsVm.Id, opt =>
                    opt.MapFrom(museumEvent => museumEvent.Id))
                .ForMember(detailsVm => detailsVm.Name, opt => 
                    opt.MapFrom(museumEvent => museumEvent.Name) )
                .ForMember(detailsVm => detailsVm.Annotation, opt =>
                    opt.MapFrom(museumEvent => museumEvent.Annotation))
                .ForMember(detailsVm => detailsVm.Description, opt =>
                    opt.MapFrom(museumEvent => museumEvent.Description))
                .ForMember(detailsVm => detailsVm.AudienceType, opt =>
                    opt.MapFrom(museumEvent => museumEvent.AudienceType))
                .ForMember(detailsVm => detailsVm.EventType, opt =>
                    opt.MapFrom(museumEvent => museumEvent.EventType))
                .ForMember(detailsVm => detailsVm.StartDate, opt =>
                    opt.MapFrom(museumEvent => museumEvent.StartDate))
                .ForMember(detailsVm => detailsVm.EndDate, opt =>
                    opt.MapFrom(museumEvent => museumEvent.EndDate))
                .ForMember(detailsVm => detailsVm.TicketLink, opt =>
                    opt.MapFrom(museumEvent => museumEvent.TicketLink))
                .ForMember(detailsVm => detailsVm.Photos, opt =>
                    opt.MapFrom(museumEvent => museumEvent.Photos));
        }
    }
}
