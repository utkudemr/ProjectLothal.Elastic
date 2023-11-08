using ProjectLothal.ElasticSearch.Domain.Enums;

namespace ProjectLothal.Elastic.Application.Features.Products.Query.GetById;

public record GetByIdProductFeatureDto(int Width, int Height, PColor Color)
{
}
