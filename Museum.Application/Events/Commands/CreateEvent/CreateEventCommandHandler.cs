using MediatR;
using Museum.Application.Interfases;
using Museum.Domain;

namespace Museum.Application.MuseumServices.Commands.CreateMuseumService
{
    public class CreateEventCommandHandler
        : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IMuseumDbContext _dbCcontext;

        public CreateEventCommandHandler(IMuseumDbContext dbCcontext) =>
            _dbCcontext = dbCcontext;

        public async Task<Guid> Handle(CreateEventCommand request,
            CancellationToken cancellationToken)
        {
            var museumService = new Event
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                CreationDate = DateTime.Now,
                UpdatedDate = null,
            };

            await _dbCcontext.Events.AddAsync(museumService);
            await _dbCcontext.saveChangesAsync(cancellationToken);
            return museumService.Id;
        }
    }
}
