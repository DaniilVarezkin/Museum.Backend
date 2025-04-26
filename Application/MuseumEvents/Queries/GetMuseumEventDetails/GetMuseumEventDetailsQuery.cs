using MediatR;


namespace Museum.Application.MuseumEvents.Queries.GetMuseumEventDetails
{
    public class GetMuseumEventDetailsQuery : IRequest<MuseumEventDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
