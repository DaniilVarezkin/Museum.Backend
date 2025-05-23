﻿using MediatR;
using Museum.Application.SQRS.MuseumEvents.Common;
using Museum.Domain;

namespace Museum.Application.SQRS.MuseumEvents.Commands.CreateMuseumEvent
{
    public class CreateMuseumEventCommand : IRequest<Guid>
    {
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Annotation { get; set; } = string.Empty;
        public AudienceType AudienceType { get; set; }
        public MuseumEventType EventType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string TicketLink { get; set; } = string.Empty;
        public ICollection<PhotoUploadDto> Photos { get; set; } = new List<PhotoUploadDto>();
    }
}
