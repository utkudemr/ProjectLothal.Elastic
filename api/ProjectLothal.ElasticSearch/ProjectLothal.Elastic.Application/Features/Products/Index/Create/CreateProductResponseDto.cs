

namespace ProjectLothal.Elastic.Application.Features.Products.Index.Create;

public record CreateProductResponseDto(string Id, string Name, decimal Price, int Stock, CreateProductFeatureDto? Feature)
{
}
