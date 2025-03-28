using Museum.Application.Common.Mappings;
using Museum.Domain;
using AutoMapper;

namespace Museum.Application.MuseumServices.Queries.GetMuseumServiceDetails
{
    public class EventVm : IMapWith<Event>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime StartEventDate { get; set; }
        public DateTime EndEventDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Event,  EventVm>()
                .ForMember(vm => vm.Title, 
                    opt => opt.MapFrom(e => e.Title))
                .ForMember(vm => vm.Description,
                    opt => opt.MapFrom(e => e.Description))
                .ForMember(vm => vm.Price,
                    opt => opt.MapFrom(e => e.Price))
                .ForMember(vm => vm.CreationDate,
                    opt => opt.MapFrom(e => e.CreationDate))
                .ForMember(vm => vm.UpdatedDate,
                    opt => opt.MapFrom(e => e.UpdatedDate))
                .ForMember(vm => vm.StartEventDate,
                    opt => opt.MapFrom(e => e.StartEventDate))
                .ForMember(vm => vm.EndEventDate,
                    opt => opt.MapFrom(e => e.EndEventDate));

        }
    }
}
