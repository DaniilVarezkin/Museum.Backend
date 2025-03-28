using MediatR;
using Microsoft.EntityFrameworkCore;
using Museum.Application.Interfases;
using Museum.Application.Common;
using Museum.Application.Common.Exceptions;
using Museum.Domain;


namespace Museum.Application.MuseumServices.Commands.UpdateMuseumService
{
    public class UpdateEventCommandHandler
        : IRequestHandler<UpdateEventCommand>
    {
        private readonly IMuseumDbContext _dbContext;

        public UpdateEventCommandHandler(IMuseumDbContext dbCcontext) =>
            _dbContext = dbCcontext;

        public async Task Handle(UpdateEventCommand request, 
            CancellationToken cancellationToken)
        {
            var entity = 
                await _dbContext.Events
                .FirstOrDefaultAsync(service =>
                    service.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Event), request.Id);
            }

            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.UpdatedDate = DateTime.Now;

            await _dbContext.saveChangesAsync(cancellationToken);
        }
    }
}
