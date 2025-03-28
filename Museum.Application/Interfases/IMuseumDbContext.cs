using Microsoft.EntityFrameworkCore;
using Museum.Domain;

namespace Museum.Application.Interfases
{
    public interface IMuseumDbContext
    {
        DbSet<MuseumService> MuseumServices { get; }
        Task<int> saveChangesAsync(CancellationToken cancelationToken);
    }
}
