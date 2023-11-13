
using ProjectLothal.Elastic.Core.Requests;

namespace ProjectLothal.Elastic.Application.Features.ECommerce.Query.GetByFilter;

public class GetECommerceByFilterRequest: PageRequest
{
    public string? CustomerFirstName { get; set; }
    public string? CustomerFullName { get; set; }
    public string? CategoryName { get; set; }
    public TaxfulTotalPriceDto? TaxfulTotalPriceDto { get; set; }

}

public record TaxfulTotalPriceDto(double GreaterThan, double LessThan)
{
}


