

using ProjectLothal.ElasticSearch.Domain.Models;

namespace ProjectLothal.Elastic.Application.Services;

public interface IProductRepository
{
    Task<Product?> IndexAsync(Product product);
}
