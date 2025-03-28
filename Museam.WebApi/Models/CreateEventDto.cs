using AutoMapper;
using Museum.Application.Common.Mappings;
using Museum.Application.MuseumServices.Commands.CreateMuseumService;

namespace Museam.WebApi.Models
{
    public class CreateEventDto : IMapWith<CreateEventCommand>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime StartEventDate { get; set; }
        public DateTime EndEventDate { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateEventDto, CreateEventCommand>()
                .ForMember(e => e.Title,
                    opt => opt.MapFrom(dto => dto.Title))
                .ForMember(e => e.Description,
                    opt => opt.MapFrom(dto => dto.Description))
                .ForMember(e => e.Price,
                    opt => opt.MapFrom(dto => dto.Price))
                .ForMember(e => e.StartEventDate,
                    opt => opt.MapFrom(dto => dto.StartEventDate))
                .ForMember(e => e.EndEventDate,
                    opt => opt.MapFrom(dto => dto.EndEventDate));
        }
    }
}
