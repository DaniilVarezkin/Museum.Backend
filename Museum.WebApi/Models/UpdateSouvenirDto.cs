using AutoMapper;
using Museum.Application.Common.Mapping;
using Museum.Application.SQRS.Souvenirs.Commands.CreateSouvenir;
using Museum.Application.SQRS.Souvenirs.Commands.UpdateSouvenir;

namespace Museum.WebApi.Models
{
    public class UpdateSouvenirDto : IMapWith<UpdateSouvenirCommand>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public IFormFile? Photo { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<UpdateSouvenirDto, UpdateSouvenirCommand>()
                .ForMember(command => command.Id, opt =>
                    opt.MapFrom(dto => dto.Id))
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
