using MediatR;
using Museum.Application.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Museum.Application.Common.Exceptions;
using Museum.Domain;

namespace Museum.Application.MuseumServices.Commands.DeleteMuseumService
{
    public class DeleteMuseumServiceCommandHandler 
        : IRequestHandler<DeleteMuseumServiceCommand>
    {
        private readonly IMuseumDbContext _dbContext;
        public DeleteMuseumServiceCommandHandler(IMuseumDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task Handle(
            DeleteMuseumServiceCommand request, 
            CancellationToken cancellationToken)
        {
            var entity =  await _dbContext.MuseumServices.FindAsync(
                new object[] { request.Id, cancellationToken });

            if (entity == null)
            {
                throw new NotFoundException(nameof(MuseumService), request.Id);
            }

            _dbContext.MuseumServices.Remove(entity);
            await _dbContext.saveChangesAsync(cancellationToken);
        }
    }
}
