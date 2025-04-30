using AutoMapper;
using Museum.Application.Common.Mapping;
using Museum.Domain;

namespace Museum.Application.MuseumEvents.Common
{
    public class EventPhotoDto : IMapWith<EventPhoto>
    {
        public Guid Id { get; set; }
        public required string FilePath {  get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<EventPhoto, EventPhotoDto>()
                .ForMember(photoDto => photoDto.Id, opt => 
                    opt.MapFrom(eventPhoto => eventPhoto.Id))
                .ForMember(photoDto => photoDto.FilePath, opt =>
                    opt.MapFrom(eventPhoto => eventPhoto.FilePath));

        }
    }
}
