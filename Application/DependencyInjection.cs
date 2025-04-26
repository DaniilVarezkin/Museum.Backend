using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using System.Threading.Tasks;

namespace Museum.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            );

            return services;
        }
    }
}
