using MediatR;


namespace Museum.Application.SQRS.MuseumEvents.Commands.DeleteMuseumEvent
{
    public class DeleteMuseumEventCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
