using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Museam.WebApi.Models;
using Museum.Application.MuseumServices.Commands.CreateMuseumService;
using Museum.Application.MuseumServices.Commands.DeleteMuseumService;
using Museum.Application.MuseumServices.Commands.UpdateMuseumService;
using Museum.Application.MuseumServices.Queries.GetMuseumServiceDetails;
using Museum.Application.MuseumServices.Queries.GetMuseumServiceList;

namespace Museam.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class EventController : BaseController
    {
        private readonly IMapper _mapper;

        public EventController(IMapper mapper) =>
            _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<EventListVm>> GetAll()
        {
            var query = new GetEventListQuery();
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventVm>> Get(Guid id)
        {
            var query = new GetEventDetailsQuery { Id = id };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            [FromBody] CreateEventDto createEventDto)
        {
            var command = _mapper.Map<CreateEventCommand>(createEventDto);
            var eventId = await Mediator.Send(command);
            return Ok(eventId);
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody] UpdateEventDto updateEventDto)
        {
            var command = _mapper.Map<UpdateEventCommand>(updateEventDto);
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteEventCommand { Id = id };
            await Mediator.Send(command);
            return NoContent();
        }


    }
}
