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
        public DbSet<EventPhoto> EventsPhoto { get; set; }
        public DbSet<Souvenir> Souvenirs { get; set; }
        public DbSet<SouvenirPhoto> SouvenirsPhoto { get; set; }


        public MuseumDbContext(DbContextOptions<MuseumDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<MuseumEvent>(new MuseumEventTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
