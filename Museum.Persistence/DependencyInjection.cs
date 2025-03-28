using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Museum.Application.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<MuseumDbContext>(options =>
                options.UseSqlite(connectionString)
            );

            services.AddScoped<IMuseumDbContext>(provider =>
            {
                var context = provider.GetService<MuseumDbContext>();
                if (context == null)
                {
                    throw new InvalidOperationException("MuseumDbContext is not registered in the service provider.");
                }
                return context;
            });

            return services;
        }
    }
}
