using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Museum.Application.SQRS.Souvenirs.Commands.CreateSouvenir;
using Museum.Application.SQRS.Souvenirs.Commands.DeleteSouvenir;
using Museum.Application.SQRS.Souvenirs.Commands.UpdateSouvenir;
using Museum.Application.SQRS.Souvenirs.Queries.GetSouvenirDetails;
using Museum.Application.SQRS.Souvenirs.Queries.GetSouvenirList;
using Museum.WebApi.Common;
using Museum.WebApi.Models;

namespace Museum.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class SouvenirController : BaseController
    {
        private readonly IMapper _mapper;

        public SouvenirController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Get the list of souvenirs
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /souvenir
        /// </remarks>
        /// <returns>Returns SouvenirListVm</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SouvenirListVm>> GetAll()
        {
            var query = new GetSouvenirListQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get the souvenir by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /souvenir/F091F1EC-ED13-4D2D-BF4A-E340403D953
        /// </remarks>
        /// <returns>Returns SouvenirDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found souvenir</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SouvenirDetailsVm>> Get(Guid id)
        {
            var query = new GetSouvenirDetailsQuery
            {
                Id = id
            };

            var result = await Mediator.Send(query); 
            return Ok(result);            
        }

        /// <summary>
        /// Creates a new souvenir
        /// </summary>
        /// <remarks>
        /// POST /souvenir
        /// Content-Type: multipart/form-data
        /// Bulk:
        /// - Name: Заголовок один
        /// - Description: Описание к событию 1
        /// - Price: 1
        /// - Cound: 0
        /// - Photo: (опционально) файл изоброажение
        /// </remarks>
        /// <param name="createSouvenirDto">Souvenir dto object</param>
        /// <returns>Returns the ID (Guid) of the created souvenir</returns>
        /// <response code="200">Returns the ID of the newly created souvenir</response>
        /// <response code="400">If the request is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> Create(
            [FromForm] CreateSouvenirDto createSouvenirDto)
        {
            var command = _mapper.Map<CreateSouvenirCommand>(createSouvenirDto);

            if(createSouvenirDto.Photo != null)
            {
                command.PhotoDto = await ConverterFormFileToPhotoDto.ConvertAsync(createSouvenirDto.Photo);
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
        /// - Description: Описание к событию 1
        /// - Price: 1
        /// - Cound: 0
        /// - Photo: (опционально) файл изоброажение
        /// </remarks>
        /// <param name="updateSouvenirDto">update souvenir dto</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="404">If souvenir not found by Id</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            [FromForm] UpdateSouvenirDto updateSouvenirDto)
        {
            var command = _mapper.Map<UpdateSouvenirCommand>(updateSouvenirDto);

            if(updateSouvenirDto.Photo != null)
            {
                command.PhotoDto = await ConverterFormFileToPhotoDto.ConvertAsync(updateSouvenirDto.Photo);
            }

            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete the souvenir by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /souvenir/F091F1EC-ED13-4D2D-BF4A-E340403D953
        /// </remarks>
        /// <param name="id">Souvenir id (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found souvenir</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteSouvenirCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
