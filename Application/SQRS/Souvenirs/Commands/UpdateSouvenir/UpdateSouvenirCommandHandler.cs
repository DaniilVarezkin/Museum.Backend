using MediatR;
using Microsoft.EntityFrameworkCore;
using Museum.Application.Common.Exceptions;
using Museum.Application.Interfaces;
using Museum.Domain;


namespace Museum.Application.SQRS.Souvenirs.Commands.UpdateSouvenir
{
    public class UpdateSouvenirCommandHandler
        : IRequestHandler<UpdateSouvenirCommand>
    {
        private readonly IMuseumDbContext _dbContext;
        private readonly IFileService _fileService;
        public UpdateSouvenirCommandHandler(IMuseumDbContext dbContext, IFileService fileService) =>
            (_dbContext, _fileService) = (dbContext, fileService);

        public async Task Handle(UpdateSouvenirCommand command,
            CancellationToken cancellationToken)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var souvenir = await _dbContext.Souvenirs.FirstOrDefaultAsync(souvenir =>
                            souvenir.Id == command.Id,
                        cancellationToken);

                    if (souvenir == null)
                    {
                        throw new NotFoundException(nameof(Souvenir), command.Id);
                    }

                    souvenir.Name = command.Name;
                    souvenir.Description = command.Description;
                    souvenir.Price = command.Price;
                    souvenir.Count = command.Count;


                    //Тут логика добавления фото сувенира
                    if (command.PhotoDto != null)
                    {
                        var lastPhotoPath = souvenir?.Photo?.FilePath;

                        var filePath = await _fileService.UploadFileAsync(command.PhotoDto.Content, command.PhotoDto.Name, cancellationToken);
                        var trimFilePath = Path.Combine("uploads", "files", Path.GetFileName(filePath));
                        if (filePath != null)
                        {
                            var souvenirPhoto = new SouvenirPhoto
                            {
                                FilePath = trimFilePath,
                                Souvenir = souvenir,
                                SouvenirId = souvenir.Id,
                            };
                            await _dbContext.SouvenirsPhoto.AddAsync(souvenirPhoto, cancellationToken);
                            souvenir.Photo = souvenirPhoto;

                            //Удаление старой фотографии
                            if (lastPhotoPath != null)
                            {
                                await _fileService.DeleteFileAsync(lastPhotoPath, cancellationToken);
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
