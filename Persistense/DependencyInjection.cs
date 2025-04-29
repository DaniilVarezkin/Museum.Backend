using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Museum.Application.Interfaces;
using Museum.Persistense.Services;

namespace Museum.Persistense
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistanse(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];

            services.AddDbContext<MuseumDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            services.AddScoped<IMuseumDbContext>(provider =>
                provider.GetRequiredService<MuseumDbContext>()
            );

            var uploadFilePath = configuration["PhotoSavingPath"];

            services.AddScoped<IFileService>(provider =>
                new FileService(uploadFilePath));

            return services;
        }
    }
}
