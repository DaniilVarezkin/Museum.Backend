using Microsoft.EntityFrameworkCore;
using Museum.Application;
using Museum.Application.Interfaces;
using Museum.Domain;
using Museum.Persistense.EntityTypeConfiguration;
namespace Museum.Persistense
{
    public class MuseumDbContext : DbContext, IMuseumDbContext
    {
        public DbSet<MuseumEvent> Events { get; set; }

        public MuseumDbContext(DbContextOptions<MuseumDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<MuseumEvent>(new MuseumEventTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
