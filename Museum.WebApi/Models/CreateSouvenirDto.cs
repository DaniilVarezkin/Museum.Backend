using AutoMapper;
using Museum.Application.Common.Mapping;
using Museum.Application.SQRS.MuseumEvents.Commands.CreateMuseumEvent;
using Museum.Application.SQRS.Souvenirs.Commands.CreateSouvenir;

namespace Museum.WebApi.Models
{
    public class CreateSouvenirDto : IMapWith<CreateSouvenirCommand>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public IFormFile? Photo { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<CreateSouvenirDto, CreateSouvenirCommand>()
                .ForMember(command => command.Name, opt =>
                    opt.MapFrom(dto => dto.Name))
                .ForMember(command => command.Description, opt =>
                    opt.MapFrom(dto => dto.Description))
                .ForMember(command => command.Price, opt =>
                    opt.MapFrom(dto => dto.Price))
                .ForMember(command => command.Count, opt =>
                    opt.MapFrom(dto => dto.Count))
                .ForMember(command => command.PhotoDto, opt =>
                    opt.Ignore());
        }
    }
}
