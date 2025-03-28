using AutoMapper;
using Museum.Application.Common.Mappings;
using Museum.Application.MuseumServices.Commands.UpdateMuseumService;

namespace Museam.WebApi.Models
{
    public class UpdateEventDto : IMapWith<UpdateEventCommand>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime StartEventDate { get; set; }
        public DateTime EndEventDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateEventDto, UpdateEventCommand>()
                .ForMember(c => c.Id, opt =>
                    opt.MapFrom(dto => dto.Id))
                .ForMember(c => c.Title, opt =>
                    opt.MapFrom(dto => dto.Title))
                .ForMember(c => c.Description, opt =>
                    opt.MapFrom(dto => dto.Description))
                .ForMember(c => c.Price, opt =>
                    opt.MapFrom(dto => dto.Price))
                .ForMember(c => c.StartEventDate, opt =>
                    opt.MapFrom(dto => dto.StartEventDate))
                .ForMember(c => c.EndEventDate, opt =>
                    opt.MapFrom(dto => dto.EndEventDate));
        }
    }
}
