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
    public class DeleteEventCommandHandler 
        : IRequestHandler<DeleteEventCommand>
    {
        private readonly IMuseumDbContext _dbContext;
        public DeleteEventCommandHandler(IMuseumDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task Handle(
            DeleteEventCommand request, 
            CancellationToken cancellationToken)
        {
            var entity =  await _dbContext.Events.FindAsync(
                new object[] { request.Id, cancellationToken });

            if (entity == null)
            {
                throw new NotFoundException(nameof(Event), request.Id);
            }

            _dbContext.Events.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
