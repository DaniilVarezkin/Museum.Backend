using MediatR;

namespace Museum.Application.MuseumEvents.Queries.GetMuseumEventList
{
    public class GetMuseumEventListQuery : IRequest<MuseumEventListVm>
    {
        //Может фильтры добавить потом
    }
}
