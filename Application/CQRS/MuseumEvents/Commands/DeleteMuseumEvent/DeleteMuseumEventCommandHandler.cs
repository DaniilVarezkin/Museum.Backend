using MediatR;
using Museum.Application.Interfaces;
using Museum.Application.Common.Exceptions;
using Museum.Domain;
using Microsoft.EntityFrameworkCore;

namespace Museum.Application.SQRS.MuseumEvents.Commands.DeleteMuseumEvent
{
    public class DeleteMuseumEventCommandHandler
        : IRequestHandler<DeleteMuseumEventCommand>
    {
        private readonly IMuseumDbContext _dbContext;
        private readonly IFileService _fileService;

        public DeleteMuseumEventCommandHandler(IMuseumDbContext dbContext, IFileService fileService) =>
            (_dbContext, _fileService) = (dbContext, fileService);

        public async Task Handle(DeleteMuseumEventCommand command,
            CancellationToken cancellationToken)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var museumEvent = await _dbContext.Events.Include(museumEvent => museumEvent.Photos)
                        .FirstOrDefaultAsync(museumEvent => museumEvent.Id == command.Id);

                    if (museumEvent == null)
                    {
                        throw new NotFoundException(nameof(MuseumEvent), command.Id);
                    }

                    _dbContext.Events.Remove(museumEvent);

                    if (museumEvent.Photos != null && museumEvent.Photos.Any())
                    {
                        await _fileService.DeleteFileRangeAsync(museumEvent.Photos.Select(p => p.FilePath), cancellationToken);
                    }

                    await transaction.CommitAsync(cancellationToken);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }
                catch(Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }
    }
}
