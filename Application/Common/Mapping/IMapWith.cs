
using AutoMapper;

namespace Museum.Application.Common.Mapping
{
    public interface IMapWith<T>
    {
        void ConfigureMapping(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType());
        }
    }
}
