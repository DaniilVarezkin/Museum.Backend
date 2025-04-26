using MediatR;
using Museum.Application.Interfaces;
using Museum.Domain;

namespace Museum.Application.MuseumEvents.Commands.CreateMuseumEvent
{
    public class CreateMuseumEventCommandHandler 
        : IRequestHandler<CreateMuseumEventCommand, Guid>
    {
        private readonly IMuseumDbContext _dbContext;
        public CreateMuseumEventCommandHandler(IMuseumDbContext dbContext) => 
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateMuseumEventCommand command,
            CancellationToken cancellationToken)
        {
            var museumEvent = new MuseumEvent
            {
                Name = command.Name,
                Description = command.Description,
                Annotation = command.Annotation,
                AudienceType = command.AudienceType,
                EventType = command.EventType,
                StartDate = command.StartDate,
                EndDate = command.EndDate,
                TicketLink = command.TicketLink,
                Photos = command.Photos,
            };

            await _dbContext.Events.AddAsync(museumEvent, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return museumEvent.Id;
        }
    }
}
