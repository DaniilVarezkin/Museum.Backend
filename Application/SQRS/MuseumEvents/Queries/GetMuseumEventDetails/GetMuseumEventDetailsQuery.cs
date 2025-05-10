using MediatR;

namespace Museum.Application.SQRS.MuseumEvents.Queries.GetMuseumEventDetails
{
    public class GetMuseumEventDetailsQuery : IRequest<MuseumEventDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
