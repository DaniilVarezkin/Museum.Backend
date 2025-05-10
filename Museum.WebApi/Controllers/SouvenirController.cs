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

        [HttpGet]
        public async Task<ActionResult<SouvenirListVm>> GetAll()
        {
            var query = new GetSouvenirListQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SouvenirDetailsVm>> Get(Guid id)
        {
            var query = new GetSouvenirDetailsQuery
            {
                Id = id
            };

            var result = await Mediator.Send(query); 
            return Ok(result);            
        }

        [HttpPost]
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

        [HttpPut]
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

        [HttpDelete("{id}")]
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
