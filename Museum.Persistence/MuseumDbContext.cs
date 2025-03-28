using Museum.Domain;
using Museum.Application;
using Microsoft.EntityFrameworkCore;
using Museum.Application.Interfases;
using Museum.Persistence.EntityTypeConfigurations;

namespace Museum.Persistence
{
    public class MuseumDbContext : DbContext, IMuseumDbContext
    {
        public DbSet<Event> Events { get; set; }
        
        public MuseumDbContext(DbContextOptions<MuseumDbContext> options) 
            : base (options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ServiceConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}
