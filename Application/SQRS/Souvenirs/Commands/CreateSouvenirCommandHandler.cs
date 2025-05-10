using MediatR;
using Museum.Application.Interfaces;
using Museum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Application.SQRS.Souvenirs.Commands
{
    public class CreateSouvenirCommandHandler
        : IRequestHandler<CreateSouvenirCommand, Guid>
    {
        private readonly IMuseumDbContext _dbContext;
        private readonly IFileService _fileService;
        public CreateSouvenirCommandHandler(IMuseumDbContext dbContext, IFileService fileService) =>
            (_dbContext, _fileService) = (dbContext, fileService);

        public async Task<Guid> Handle(CreateSouvenirCommand command,
            CancellationToken cancellationToken)
        {
            using(var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var souvenir = new Souvenir
                    {
                        Name = command.Name,
                        Description = command.Description,
                        Price = command.Price,
                        Count = command.Count,
                    };

                    if (command.PhotoDto != null)
                    {
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
                        }
                    }

                    await _dbContext.Souvenirs.AddAsync(souvenir, cancellationToken);

                    await _dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    return souvenir.Id;
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
