using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Museum.Application.SQRS.MuseumEvents.Commands.CreateMuseumEvent;
using Museum.Application.SQRS.MuseumEvents.Commands.DeleteMuseumEvent;
using Museum.Application.SQRS.MuseumEvents.Commands.UpdateMuseumEvent;
using Museum.Application.SQRS.MuseumEvents.Common;
using Museum.Application.SQRS.MuseumEvents.Queries.GetMuseumEventDetails;
using Museum.Application.SQRS.MuseumEvents.Queries.GetMuseumEventList;
using Museum.WebApi.Common;
using Museum.WebApi.Models;

namespace Museum.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class MuseumController : BaseController
    {
        private readonly IMapper _mapper;

        public MuseumController(IMapper mapper) =>
            _mapper = mapper;


        /// <summary>
        /// Get the list of museum events
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /museum
        /// </remarks>
        /// <returns>Returns NoteListVm</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MuseumEventListVm>> GetAll()
        {
            var query = new GetMuseumEventListQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get the museum event by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /museum/F091F1EC-ED13-4D2D-BF4A-E340403D953
        /// </remarks>
        /// <returns>Returns MuseumEventDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found museum event</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MuseumEventDetailsVm>> Get(Guid id)
        {
            var query = new GetMuseumEventDetailsQuery
            {
                Id = id,
            };

            var result = await Mediator.Send(query);
            return Ok(result);
        }


        /// <summary>
        /// Creates a new museum event
        /// </summary>
        /// <remarks>
        /// POST /museum
        /// Content-Type: multipart/form-data
        /// Bulk:
        /// - Name: Заголовок один
        /// - Annotation: Аннотация к событию 1
        /// - Description: Описание к событию 1
        /// - AudienceType: 1
        /// - EventType: 0
        /// - StartDate: 2024-05-20T14:30:00
        /// - TicketLink: https://example.com
        /// - Photos: (опционально) один или несколько файлов изображений
        /// </remarks>
        /// <param name="museumEventDto">Museum event data including optional photos</param>
        /// <returns>Returns the ID (Guid) of the created event</returns>
        /// <response code="200">Returns the ID of the newly created event</response>
        /// <response code="400">If the request is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromForm] CreateMuseumEventDto museumEventDto)
        {
            var command = _mapper.Map<CreateMuseumEventCommand>(museumEventDto);

            if(museumEventDto.Photos != null && museumEventDto.Photos.Any())
            {
                command.Photos = await ConverterFormFileToPhotoDto.ConvertRangeAsync(museumEventDto.Photos);
            }

            var result = await Mediator.Send(command);

            return Ok(result);
        }


        /// <summary>
        /// Creates a new museum event
        /// </summary>
        /// <remarks>
        /// PUT /museum
        /// Content-Type: multipart/form-data
        /// Bulk:
        /// - Id: 79396c57-f07c-4dfa-af5b-b5a7e5aa258fб
        /// - Name: Заголовок один
        /// - Annotation: Аннотация к событию 1
        /// - Description: Описание к событию 1
        /// - AudienceType: 1
        /// - EventType: 0
        /// - StartDate: 2024-05-20T14:30:00
        /// - TicketLink: https://example.com
        /// - AddedPhotos: (опционально) один или несколько файлов изображений
        /// - DeletedPhotos: 79396c57-f07c-4dfa-af5b-b5a7e5aa258fб, 80396c57-f07c-4dfa-af5b-b5a7e5aa257j
        /// </remarks>
        /// <param name="UpdateMuseumEventDto">Museum event data including optional photos</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="404">If museum event not found by Id</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            [FromForm] UpdateMuseumEventDto updateMuseumEventDto)
        {
            var command = _mapper.Map<UpdateMuseumEventCommand>(updateMuseumEventDto);

            if(updateMuseumEventDto.AddedPhotos != null && updateMuseumEventDto.AddedPhotos.Any())
            {
                command.AddedPhotos = await ConverterFormFileToPhotoDto.ConvertRangeAsync(updateMuseumEventDto.AddedPhotos);
            }

            await Mediator.Send(command);

            return NoContent();
        }


        /// <summary>
        /// Delete the museum event by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /museum/F091F1EC-ED13-4D2D-BF4A-E340403D953
        /// </remarks>
        /// <param name="id">Museum Event id (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found museum event</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var command = new DeleteMuseumEventCommand
            {
                Id = Id
            };
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
