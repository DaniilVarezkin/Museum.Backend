using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class EventPhoto
    {
        public Guid Id { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public MuseumEvent? MuseumEvent { get; set; } = null;

        [ForeignKey(nameof(MuseumEvent))]
        public Guid MuseumEventId { get; set; }
    }
}
