using MediatR;
using Museum.Application.SQRS.MuseumEvents.Common;


namespace Museum.Application.SQRS.Souvenirs.Commands.UpdateSouvenir
{
    public class UpdateSouvenirCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public PhotoUploadDto? PhotoDto { get; set; } = null;
    }
}
