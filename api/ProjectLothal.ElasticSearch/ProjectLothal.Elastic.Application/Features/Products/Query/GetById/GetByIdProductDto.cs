

namespace ProjectLothal.Elastic.Application.Features.Products.Query.GetById
{
    public record GetByIdProductDto(string Id, string Name, decimal Price, int Stock, GetByIdProductFeatureDto? Feature)
    {

    }
}
