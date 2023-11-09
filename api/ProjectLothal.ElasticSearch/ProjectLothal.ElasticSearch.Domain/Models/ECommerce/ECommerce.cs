

using Nest;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace ProjectLothal.ElasticSearch.Domain.Models.ECommerceModel;

public class ECommerce
{
    [Text(Name = "_id")]
    public string Id { get; set; } = null!;

    [Text(Name = "customer_first_name")]
    public string CustomerFirstName { get; set; } = null!;

    [Text(Name = "customer_last_name")]
    public string CustomerLastName { get; set; } = null!;

    [Text(Name = "customer_full_name")]
    public string CustomerFullName { get; set; } = null!;

    [DoubleRange(Name = "taxful_total_price")]
    public double TaxfulTotalPrice { get; set; }
    [Text(Name = "category")]
    public string[] Category { get; set; } = null!;

    [Number(NumberType.Byte, Name = "order_id")]
    public int OrderId { get; set; }

    [Date(Format = "MMddyyyy", Name = "order_date")]
    public DateTime OrderDate { get; set; }

    [Nested]
    [JsonPropertyName("products")]
    public Product[] Products { get; set; }
}

public class Product

{

    [LongRange(Name = "product_id")]
    public long ProductId { get; set; }

    [Text(Name = "product_name")]
    public string ProductName { get; set; }
}