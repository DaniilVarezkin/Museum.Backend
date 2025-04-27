using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Museum.Domain;

namespace Museum.Application.Interfaces
{
    public interface IMuseumDbContext
    {
        DatabaseFacade Database { get; }
        DbSet<MuseumEvent> Events { get; set; }
        DbSet<EventPhoto> EventsPhoto { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
