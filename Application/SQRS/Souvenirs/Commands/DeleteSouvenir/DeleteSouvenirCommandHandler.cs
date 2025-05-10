using MediatR;
using Microsoft.EntityFrameworkCore;
using Museum.Application.Common.Exceptions;
using Museum.Application.Interfaces;
using Museum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Application.SQRS.Souvenirs.Commands.DeleteSouvenir
{
    public class DeleteSouvenirCommandHandler 
        : IRequestHandler<DeleteSouvenirCommand>
    {
        private readonly IMuseumDbContext _dbContext;
        private readonly IFileService _fileService;

        public DeleteSouvenirCommandHandler(IMuseumDbContext dbContext, IFileService fileService) =>
            (_dbContext, _fileService) = (dbContext, fileService);

        public async Task Handle(DeleteSouvenirCommand command, CancellationToken cancellationToken)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var souvenir = await _dbContext.Souvenirs.Include(souvenir => souvenir.Photo)
                        .FirstOrDefaultAsync(souvenir => souvenir.Id == command.Id);

                    if (souvenir == null)
                    {
                        throw new NotFoundException(nameof(Souvenir), command.Id);
                    }

                    if (souvenir.Photo != null)
                    {
                        await _fileService.DeleteFileAsync(souvenir.Photo.FilePath, cancellationToken);
                    }
                    _dbContext.Souvenirs.Remove(souvenir);

                    await transaction.CommitAsync(cancellationToken);
                    await _dbContext.SaveChangesAsync(cancellationToken);
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
