using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Museam.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => 
            _mediator ??= (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));

        
    }
}
