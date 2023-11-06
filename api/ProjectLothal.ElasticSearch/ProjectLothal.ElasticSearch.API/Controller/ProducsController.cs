using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using ProjectLothal.Elastic.Application.Features.Products.Index.Create;

namespace ProjectLothal.ElasticSearch.API.Controller
{
    [Route("api/[controller]")]
    public class ProducsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProducsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("saveProduct")]
        public async Task<IActionResult> SaveProduct([FromBody]CreateProductDto request, CancellationToken cancellationToken)
        {
            var result = await _mediator.SendRequest(new CreateProductStatus { Request = request });
            return Ok(result);
        }
    }
}
