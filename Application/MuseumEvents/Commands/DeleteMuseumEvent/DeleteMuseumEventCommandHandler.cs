using MediatR;
using Museum.Application.Interfaces;
using Museum.Application.Common.Exceptions;
using Museum.Domain;

namespace Museum.Application.MuseumEvents.Commands.DeleteMuseumEvent
{
    public class DeleteMuseumEventCommandHandler
        : IRequestHandler<DeleteMuseumEventCommand>
    {
        private readonly IMuseumDbContext _dbContext;

        public DeleteMuseumEventCommandHandler(IMuseumDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task Handle(DeleteMuseumEventCommand command,
            CancellationToken cancellationToken)
        {
            var museumEvent = await _dbContext.Events
                .FindAsync(new object[] {command.Id});

            if( museumEvent == null)
            {
                throw new NotFoundException(nameof(MuseumEvent), command.Id);
            }

            _dbContext.Events.Remove(museumEvent);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
