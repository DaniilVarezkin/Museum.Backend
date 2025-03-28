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
                Price = request.Price,
                CreationDate = DateTime.Now,
                UpdatedDate = null,
                StartEventDate = request.StartEventDate,
                EndEventDate = request.EndEventDate
            };

            await _dbCcontext.Events.AddAsync(museumService);
            await _dbCcontext.SaveChangesAsync(cancellationToken);
            return museumService.Id;
        }
    }
}
