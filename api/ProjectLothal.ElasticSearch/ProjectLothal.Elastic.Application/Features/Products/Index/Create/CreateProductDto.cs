using MassTransit.Mediator;
using ProjectLothal.Elastic.Core.Responses;

namespace ProjectLothal.Elastic.Application.Features.Products.Index.Create
{
    public record CreateProductDto(string Name, decimal Price, int Stock, CreateProductFeatureDto FeatureDto )
    {

    }
}
