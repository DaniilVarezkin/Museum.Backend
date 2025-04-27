using Microsoft.EntityFrameworkCore;
using Museum.Domain;

namespace Museum.Application.Interfaces
{
    public interface IMuseumDbContext
    {
        DbSet<MuseumEvent> Events { get; set; }
        DbSet<EventPhoto> EventsPhoto { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
