using MediatR;


namespace Museum.Application.MuseumEvents.Commands.DeleteMuseumEvent
{
    public class DeleteMuseumEventCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
