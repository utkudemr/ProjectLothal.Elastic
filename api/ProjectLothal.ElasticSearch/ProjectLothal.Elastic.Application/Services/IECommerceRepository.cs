

using Nest;
using ECommerceModel = ProjectLothal.ElasticSearch.Domain.Models.ECommerceModel;

namespace ProjectLothal.Elastic.Application.Services;

public interface IECommerceRepository
{
    Task<IReadOnlyCollection<ECommerceModel.ECommerce>?> TermQuery(List<Func<QueryContainerDescriptor<ECommerceModel.ECommerce>, QueryContainer>> filter);
}
