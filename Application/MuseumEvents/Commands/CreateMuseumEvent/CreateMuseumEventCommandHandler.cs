using MediatR;
using Museum.Application.Interfaces;
using Museum.Domain;

namespace Museum.Application.MuseumEvents.Commands.CreateMuseumEvent
{
    public class CreateMuseumEventCommandHandler 
        : IRequestHandler<CreateMuseumEventCommand, Guid>
    {
        private readonly IMuseumDbContext _dbContext;
        private readonly IFileService _fileService;
        public CreateMuseumEventCommandHandler(IMuseumDbContext dbContext, IFileService fileService) => 
            (_dbContext, _fileService) = (dbContext, fileService);

        public async Task<Guid> Handle(CreateMuseumEventCommand command,
            CancellationToken cancellationToken)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
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
                    };

                    if (command.Photos != null && command.Photos.Any())
                    {
                        foreach (var photoDto in command.Photos)
                        {
                            var filePath = await _fileService.UploadFileAsync(photoDto.Content, photoDto.Name, cancellationToken);
                            if (filePath != null)
                            {
                                var eventPhoto = new EventPhoto
                                {
                                    FilePath = filePath,
                                    MuseumEvent = museumEvent,
                                    MuseumEventId = museumEvent.Id,
                                };
                                await _dbContext.EventsPhoto.AddAsync(eventPhoto, cancellationToken);
                                museumEvent.Photos.Add(eventPhoto);
                            }
                        }
                    }

                    await _dbContext.Events.AddAsync(museumEvent, cancellationToken);

                    await _dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    return museumEvent.Id;
                } catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }
    }
}
