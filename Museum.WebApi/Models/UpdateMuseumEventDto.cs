using AutoMapper;
using Museum.Application.Common.Mapping;
using Museum.Domain;
using Museum.Application.SQRS.MuseumEvents.Commands.UpdateMuseumEvent;

namespace Museum.WebApi.Models
{
    public class UpdateMuseumEventDto : IMapWith<UpdateMuseumEventCommand>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Annotation { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public AudienceType AudienceType { get; set; }
        public MuseumEventType EventType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } = null;
        public string TicketLink { get; set; } = string.Empty;
        public ICollection<IFormFile>? AddedPhotos { get; set; } = new List<IFormFile>();
        public ICollection<Guid>? DeletedPhoto {  get; set; } = new List<Guid>();


        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<UpdateMuseumEventDto, UpdateMuseumEventCommand>()
                .ForMember(command => command.Id, opt =>
                    opt.MapFrom(dto => dto.Id))
                .ForMember(command => command.Name, opt =>
                    opt.MapFrom(dto => dto.Name))
                .ForMember(command => command.Annotation, opt =>
                    opt.MapFrom(dto => dto.Annotation))
                .ForMember(command => command.Description, opt =>
                    opt.MapFrom(dto => dto.Description))
                .ForMember(command => command.AudienceType, opt =>
                    opt.MapFrom(dto => dto.AudienceType))
                .ForMember(command => command.EventType, opt =>
                    opt.MapFrom(dto => dto.EventType))
                .ForMember(command => command.StartDate, opt =>
                    opt.MapFrom(dto => dto.StartDate))
                .ForMember(command => command.EndDate, opt =>
                    opt.MapFrom(dto => dto.EndDate))
                .ForMember(command => command.TicketLink, opt =>
                    opt.MapFrom(dto => dto.TicketLink))
                .ForMember(command => command.DeletedPhotosIds, opt =>
                    opt.MapFrom(dto => dto.DeletedPhoto))
                .ForMember(command => command.AddedPhotos, opt =>
                    opt.Ignore());
        }
    }
}
