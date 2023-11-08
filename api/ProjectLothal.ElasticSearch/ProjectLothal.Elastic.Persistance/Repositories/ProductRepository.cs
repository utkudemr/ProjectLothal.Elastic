

using Nest;
using ProjectLothal.Elastic.Application.Services;
using ProjectLothal.ElasticSearch.Domain.Models;
using System.Collections.Immutable;

namespace ProjectLothal.Elastic.Persistance.Repositories;

public class ProductRepository: IProductRepository
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

        var productCreateResponse = await _client.IndexAsync<Product>(product,a=>a.Index(ProductIndexName).Id(Guid.NewGuid().ToString()));

        if (!productCreateResponse.IsValid) return null;

        product.Id = productCreateResponse.Id;
        return product;
    }

    public async Task<IReadOnlyCollection<Product>?> GetAllAsync()
    {
        var productList = await _client.SearchAsync<Product>(s => s.Index(ProductIndexName).Query(a => a.MatchAll()));

        foreach (var hit in productList.Hits) hit.Source.Id = hit.Id;
        return productList.Documents.ToImmutableList();
    }

    public async Task<Product?> GetByIdAsync(string id)
    {
        var product = await _client.GetAsync<Product>(id,x=>x.Index(ProductIndexName));
        if (!product.IsValid) return null;
        product.Source.Id = product.Id;
        return product.Source;
    }
}
