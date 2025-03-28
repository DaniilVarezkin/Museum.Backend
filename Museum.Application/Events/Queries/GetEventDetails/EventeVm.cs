using Museum.Application.Common.Mappings;
using Museum.Domain;
using AutoMapper;

namespace Museum.Application.MuseumServices.Queries.GetMuseumServiceDetails
{
    public class EventeVm : IMapWith<Event>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Event,  EventeVm>()
                .ForMember(vm => vm.Title, 
                    opt => opt.MapFrom(ms => ms.Title))
                .ForMember(vm => vm.Description,
                    opt => opt.MapFrom(ms => ms.Description))
                .ForMember(vm => vm.Price,
                    opt => opt.MapFrom(ms => ms.Price))
                .ForMember(vm => vm.CreationDate,
                    opt => opt.MapFrom(ms => ms.CreationDate))
                .ForMember(vm => vm.UpdatedDate,
                    opt => opt.MapFrom(ms => ms.UpdatedDate));
        }
    }
}
