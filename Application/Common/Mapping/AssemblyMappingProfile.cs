using AutoMapper;
using System.Reflection;

namespace Museum.Application.Common.Mapping
{
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile(Assembly assembly) =>
            ApplyMappingConfigurationFromAssembly(assembly);

        private void ApplyMappingConfigurationFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType && 
                         i.GetGenericTypeDefinition() == typeof(IMapWith<>))
                    ).ToList();

            foreach ( var type in types )
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod(nameof(IMapWith<object>.ConfigureMapping));

                methodInfo?.Invoke(instance, [this]);
            }
        }
    }
}
