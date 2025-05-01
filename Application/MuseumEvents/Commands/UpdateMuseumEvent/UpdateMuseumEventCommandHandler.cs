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
        private readonly IFileService _fileService;
        public UpdateMuseumEventCommandHandler(IMuseumDbContext dbContext, IFileService fileService) =>
            (_dbContext, _fileService) = (dbContext, fileService);

        public async Task Handle(UpdateMuseumEventCommand command,
            CancellationToken cancellationToken)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var museumEvent = await _dbContext.Events.FirstOrDefaultAsync(museumEvent => 
                            museumEvent.Id == command.Id,
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

                    //Тут логика удаления фото мероприятия
                    if(command.DeletedPhotosIds != null && command.DeletedPhotosIds.Any())
                    {
                        var photoToDelete = await _dbContext.EventsPhoto.Where(photo =>
                            command.DeletedPhotosIds.Contains(photo.Id)).ToListAsync(cancellationToken);

                        if(photoToDelete != null && photoToDelete.Any()) 
                        {
                            // Удаляем файлы параллельно
                            await _fileService.DeleteFileRangeAsync(
                                photoToDelete.Select(p => p.FilePath),
                                cancellationToken);

                            //удаляем из бд
                            _dbContext.EventsPhoto.RemoveRange(photoToDelete);
                        }
                    }


                    //Тут логика добавления фото мероприятия
                    if (command.AddedPhotos != null && command.AddedPhotos.Any())
                    {
                        foreach (var photoDto in command.AddedPhotos)
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


                    await _dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }
    }
}
