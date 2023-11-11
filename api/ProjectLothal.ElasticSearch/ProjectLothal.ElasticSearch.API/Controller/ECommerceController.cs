using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ProjectLothal.Elastic.Application.Features.ECommerce.Query.GetByFilter;
using ProjectLothal.Elastic.Application.Features.ECommerce.Query.GetById;

namespace ProjectLothal.ElasticSearch.API.Controller;

public class ECommerceController :   BaseController
{
    [HttpPost("getFilter")]
    public async Task<IActionResult> GetAllProduct([FromBody] GetECommerceByFilterRequest? request, CancellationToken cancellationToken)
    {
        var result = await Mediator.SendRequest(new GetECommerceByFilterStatus
        { 
            GetECommerceByFilterRequest = request,
        });
        return CreateActionResult(result);
    }
}