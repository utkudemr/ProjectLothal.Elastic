using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ProjectLothal.Elastic.Application.Features.ECommerce.Query.GetById;

namespace ProjectLothal.ElasticSearch.API.Controller;

public class ECommerceController :   BaseController
{
    [HttpPost("getFilter")]
    public async Task<IActionResult> GetAllProduct([FromQuery] string CustomerFirstName, CancellationToken cancellationToken)
    {
        var result = await Mediator.SendRequest(new GetECommerceByFilterStatus { CustomerFirstName = CustomerFirstName });
        return CreateActionResult(result);
    }
}