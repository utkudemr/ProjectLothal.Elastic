using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ProjectLothal.Elastic.Application.Features.Products.Index.Create;
using ProjectLothal.Elastic.Application.Features.Products.Query.GetAll;
using ProjectLothal.Elastic.Application.Features.Products.Query.GetById;

namespace ProjectLothal.ElasticSearch.API.Controller
{
    [Route("api/[controller]")]
    public class ProducsController : BaseController
    {
        [HttpPost("saveProduct")]
        public async Task<IActionResult> SaveProduct([FromBody]CreateProductDto request, CancellationToken cancellationToken)
        {
            var result = await Mediator.SendRequest(new CreateProductStatus { Request = request });
            return CreateActionResult(result);
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetAllProduct( CancellationToken cancellationToken)
        {
            var result = await Mediator.SendRequest(new GetAllProductStatus {  });
            return CreateActionResult(result);
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetAllProduct([FromQuery] string productId,CancellationToken cancellationToken)
        {
            var result = await Mediator.SendRequest(new GetProductByIdStatus { ProductId=productId});
            return CreateActionResult(result);
        }
    }
}
