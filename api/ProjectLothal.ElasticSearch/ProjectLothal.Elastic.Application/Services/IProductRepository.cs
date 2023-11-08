

using ProjectLothal.ElasticSearch.Domain.Models;

namespace ProjectLothal.Elastic.Application.Services;

public interface IProductRepository
{
    Task<Product?> IndexAsync(Product product);
    Task<IReadOnlyCollection<Product>?> GetAllAsync();
    Task<Product?> GetByIdAsync(string id);
}
