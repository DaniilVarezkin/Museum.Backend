using MediatR;

namespace Museum.Application.SQRS.MuseumEvents.Queries.GetMuseumEventList
{
    public class GetMuseumEventListQuery : IRequest<MuseumEventListVm>
    {
        //Может фильтры добавить потом
    }
}
