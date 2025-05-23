﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Domain
{
    public class MuseumEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Annotation { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public AudienceType AudienceType {  get; set; }
        public  MuseumEventType EventType {  get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string TicketLink { get; set; } = string.Empty;
        public ICollection<EventPhoto> Photos { get; set; } = new List<EventPhoto>();
    }
}
