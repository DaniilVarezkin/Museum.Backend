using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicaton(
            this IServiceCollection services)
        {
            services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(
                    Assembly.GetExecutingAssembly()
            ));
            return services;
        }
    }
}
