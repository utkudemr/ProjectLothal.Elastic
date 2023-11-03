

using Nest;
using ProjectLothal.ElasticSearch.Domain.Models;

namespace ProjectLothal.Elastic.Persistance.Repositories;

public class ProductRepository
{
    private readonly ElasticClient _client;
    private const string ProductIndexName = "products";

    public ProductRepository(ElasticClient client)
    {
        _client = client;
    }

    public async Task<Product?> IndexAsync(Product product)
    {   
        product.Created= DateTime.Now;

        var productCreateResponse = await _client.IndexAsync<Product>(product,a=>a.Index(ProductIndexName));

        if (productCreateResponse.IsValid) return null;

        product.Id = productCreateResponse.Id;
        return product;
    }
}
