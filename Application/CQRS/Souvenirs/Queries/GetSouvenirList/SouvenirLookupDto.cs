using AutoMapper;
using Museum.Application.Common.Mapping;
using Museum.Application.SQRS.MuseumEvents.Common;
using Museum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Application.SQRS.Souvenirs.Queries.GetSouvenirList
{
    public class SouvenirLookupDto : IMapWith<Souvenir>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public PhotoDto? Photo { get; set; } = null;

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Souvenir,  SouvenirLookupDto>()
                .ForMember(dto => dto.Id, opt => 
                    opt.MapFrom(souvenir => souvenir.Id))
                .ForMember(dto => dto.Name, opt =>
                    opt.MapFrom(souvenir => souvenir.Name))
                .ForMember(dto => dto.Description, opt =>
                    opt.MapFrom(souvenir => souvenir.Description))
                .ForMember(dto => dto.Price, opt =>
                    opt.MapFrom(souvenir => souvenir.Price))
                .ForMember(dto => dto.Count, opt =>
                    opt.MapFrom(souvenir => souvenir.Count))
                .ForMember(dto => dto.Photo, opt =>
                    opt.MapFrom(souvenir => souvenir.Photo));
        }
    }
}
