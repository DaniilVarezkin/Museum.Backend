using Microsoft.EntityFrameworkCore;
using Museum.Domain;

namespace Museum.Application.Interfases
{
    public interface IMuseumDbContext
    {
        DbSet<Event> Events { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
