using Museum.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Museum.Persistense.EntityTypeConfiguration
{
    public class MuseumEventTypeConfiguration : IEntityTypeConfiguration<MuseumEvent>
    {
        public void Configure(EntityTypeBuilder<MuseumEvent> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id).IsUnique();
            builder.Property(e => e.Name).HasMaxLength(256);
        }
    }
}
