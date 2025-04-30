using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Museum.Application.MuseumEvents.Commands.CreateMuseumEvent;
using Museum.Application.MuseumEvents.Commands.DeleteMuseumEvent;
using Museum.Application.MuseumEvents.Commands.UpdateMuseumEvent;
using Museum.Application.MuseumEvents.Common;
using Museum.Application.MuseumEvents.Queries.GetMuseumEventDetails;
using Museum.Application.MuseumEvents.Queries.GetMuseumEventList;
using Museum.WebApi.Models;

namespace Museum.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class MuseumController : BaseController
    {
        private readonly IMapper _mapper;

        public MuseumController(IMapper mapper) =>
            _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<MuseumEventListVm>> GetAll()
        {
            var query = new GetMuseumEventListQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<MuseumEventDetailsVm>> Get(Guid id)
        {
            var query = new GetMuseumEventDetailsQuery
            {
                Id = id,
            };

            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateMuseumEventDto museumEventDto)
        {
            var command = _mapper.Map<CreateMuseumEventCommand>(museumEventDto);

            if(museumEventDto.Photos != null && museumEventDto.Photos.Any())
            {
                command.Photos = await ConvertPhotosAsync(museumEventDto.Photos);
            }

            var result = await Mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            [FromForm] UpdateMuseumEventDto updateMuseumEventDto)
        {
            var command = _mapper.Map<UpdateMuseumEventCommand>(updateMuseumEventDto);

            if(updateMuseumEventDto.AddedPhotos != null && updateMuseumEventDto.AddedPhotos.Any())
            {
                command.AddedPhotos = await ConvertPhotosAsync(updateMuseumEventDto.AddedPhotos);
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var command = new DeleteMuseumEventCommand
            {
                Id = Id
            };
            await Mediator.Send(command);

            return NoContent();
        }



        private async Task<List<EventPhotoUploadDto>> ConvertPhotosAsync(ICollection<IFormFile> photos)
        {
            var result = new List<EventPhotoUploadDto>();
            foreach (var photo in photos)
            {
                var memoryStream = new MemoryStream();
                await photo.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                result.Add(new EventPhotoUploadDto
                {
                    Content = memoryStream,
                    Name = photo.FileName,
                });
            }
            return result;
        }


    }
}
