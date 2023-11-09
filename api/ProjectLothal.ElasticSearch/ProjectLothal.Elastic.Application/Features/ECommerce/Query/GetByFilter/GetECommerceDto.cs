

namespace ProjectLothal.Elastic.Application.Features.ECommerce.Query.GetById;

public class GetECommerceDto
{
    public string Id { get; set; } = null!;
    public string CustomerFirstName { get; set; } = null!;
    public string CustomerLastName { get; set; } = null!;
    public string CustomerFullName { get; set; } = null!;
    public string[] Category { get; set; } = null!;
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
}
