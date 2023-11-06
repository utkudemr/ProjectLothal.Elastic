
using ProjectLothal.ElasticSearch.Domain.Enums;

namespace ProjectLothal.Elastic.Application.Features.Products.Index.Create
{
    public record CreateProductFeatureDto(int Width, int Height, PColor Color)
    {
    }
}
