using MediatR;
using Microsoft.EntityFrameworkCore;
using Museum.Application.Interfaces;
using Museum.Application.Common.Exceptions;
using Museum.Domain;

namespace Museum.Application.MuseumEvents.Commands.UpdateMuseumEvent
{
    public class UpdateMuseumEventCommandHandler 
        : IRequestHandler<UpdateMuseumEventCommand>
    {
        private readonly IMuseumDbContext _dbContext;
        public UpdateMuseumEventCommandHandler(IMuseumDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task Handle(UpdateMuseumEventCommand command,
            CancellationToken cancellationToken)
        {
            var museumEvent = await _dbContext.Events.FirstOrDefaultAsync(
                museumEvent => museumEvent.Id == command.Id,
                cancellationToken);

            if (museumEvent == null)
            {
                throw new NotFoundException(nameof(MuseumEvent), command.Id);
            }

            museumEvent.Name = command.Name;
            museumEvent.Description = command.Description;
            museumEvent.Annotation = command.Annotation;
            museumEvent.StartDate = command.StartDate; 
            museumEvent.EndDate = command.EndDate;
            museumEvent.EventType = command.EventType;
            museumEvent.AudienceType = command.AudienceType;
            museumEvent.TicketLink = command.TicketLink;
            museumEvent.Photos = command.Photos;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
