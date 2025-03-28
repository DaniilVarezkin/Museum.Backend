using MediatR;
using Museum.Application.Interfases;
using Museum.Domain;

namespace Museum.Application.MuseumServices.Commands.CreateMuseumService
{
    public class CreateMuseumServiceCommandHandler
        : IRequestHandler<CreateMuseumServiceCommand, Guid>
    {
        private readonly IMuseumDbContext _dbCcontext;

        public CreateMuseumServiceCommandHandler(IMuseumDbContext dbCcontext) =>
            _dbCcontext = dbCcontext;

        public async Task<Guid> Handle(CreateMuseumServiceCommand request,
            CancellationToken cancellationToken)
        {
            var museumService = new MuseumService
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                CreationDate = DateTime.Now,
                UpdatedDate = null,
            };

            await _dbCcontext.MuseumServices.AddAsync(museumService);
            await _dbCcontext.saveChangesAsync(cancellationToken);
            return museumService.Id;
        }
    }
}
