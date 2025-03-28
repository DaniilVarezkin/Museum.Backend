using MediatR;
using Microsoft.EntityFrameworkCore;
using Museum.Application.Interfases;
using Museum.Application.Common;
using Museum.Application.Common.Exceptions;
using Museum.Domain;


namespace Museum.Application.MuseumServices.Commands.UpdateMuseumService
{
    public class UpdateMuseumServiceCommandHandler
        : IRequestHandler<UpdateMuseumServiceCommand>
    {
        private readonly IMuseumDbContext _dbContext;

        public UpdateMuseumServiceCommandHandler(IMuseumDbContext dbCcontext) =>
            _dbContext = dbCcontext;

        public async Task Handle(UpdateMuseumServiceCommand request, 
            CancellationToken cancellationToken)
        {
            var entity = 
                await _dbContext.MuseumServices
                .FirstOrDefaultAsync(service =>
                    service.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(MuseumService), request.Id);
            }

            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.UpdatedDate = DateTime.Now;

            await _dbContext.saveChangesAsync(cancellationToken);
        }
    }
}
