

using Nest;
using ProjectLothal.Elastic.Core.Paging;
using ECommerceModel = ProjectLothal.ElasticSearch.Domain.Models.ECommerceModel;

namespace ProjectLothal.Elastic.Application.Services;

public interface IECommerceRepository
{
    Task<Paginate<ECommerceModel.ECommerce>?> TermQuery(
       List<Func<QueryContainerDescriptor<ECommerceModel.ECommerce>, QueryContainer>> filter,
       Func<SortDescriptor<ECommerceModel.ECommerce>, IPromise<IList<ISort>>> selector,
       int size,
       int page

       );
}