using MediatR;


namespace Museum.Application.MuseumServices.Commands.UpdateMuseumService
{
    public class UpdateEventCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int Price { get; set; }
        public DateTime StartEventDate { get; set; }
        public DateTime EndEventDate { get; set; }
    }
}
