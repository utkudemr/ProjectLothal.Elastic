
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using ProjectLothal.Elastic.Core.Responses;
using System.Net;

namespace ProjectLothal.ElasticSearch.API.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class BaseController:ControllerBase
    {
        private readonly IMediator? _mediator;
        protected IMediator? Mediator => _mediator ?? HttpContext.RequestServices.GetService<IMediator>();

        [NonAction]
        public IActionResult CreateActionResult<T>(BaseResponse<T> response)
        {
            if (response.Status == HttpStatusCode.NoContent)
                return new ObjectResult(null) { StatusCode = response.Status.GetHashCode() };
            return new ObjectResult(response) {  StatusCode = response.Status.GetHashCode() };
        }
    }
}
