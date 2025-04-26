using MediatR;
using Museum.Domain;

namespace Museum.Application.MuseumEvents.Commands.UpdateMuseumEvent
{
    public class UpdateMuseumEventCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Annotation { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public AudienceType AudienceType { get; set; }
        public MuseumEventType EventType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string TicketLink { get; set; } = string.Empty;
        public ICollection<EventPhoto> Photos { get; set; } = new List<EventPhoto>();
    }
}
