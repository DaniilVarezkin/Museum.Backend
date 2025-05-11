using AutoMapper;
using Museum.Application.Common.Mapping;
using Museum.Domain;

namespace Museum.Application.SQRS.MuseumEvents.Common
{
    public class PhotoDto : IMapWith<EventPhoto>, IMapWith<SouvenirPhoto>
    {
        public Guid Id { get; set; }
        public required string FilePath {  get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<EventPhoto, PhotoDto>()
                .ForMember(photoDto => photoDto.Id, opt => 
                    opt.MapFrom(eventPhoto => eventPhoto.Id))
                .ForMember(photoDto => photoDto.FilePath, opt =>
                    opt.MapFrom(eventPhoto => eventPhoto.FilePath));

            profile.CreateMap<SouvenirPhoto, PhotoDto>()
                .ForMember(photoDto => photoDto.Id, opt =>
                    opt.MapFrom(souvenirPhoto => souvenirPhoto.Id))
                .ForMember(photoDto => photoDto.FilePath, opt =>
                    opt.MapFrom(souvenirPhoto => souvenirPhoto.FilePath));
        }
    }
} 
