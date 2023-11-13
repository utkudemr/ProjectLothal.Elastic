

using Nest;
using ProjectLothal.Elastic.Core.Paging;
using ProjectLothal.ElasticSearch.Domain.Models.ECommerceModel;
using System.Runtime.InteropServices;
using ECommerceModel = ProjectLothal.ElasticSearch.Domain.Models.ECommerceModel;

namespace ProjectLothal.Elastic.Application.Services;

public interface IECommerceRepository
{
    Task<Paginate<ECommerce>?> TermQuery(

          [Optional] List<Func<QueryContainerDescriptor<ECommerce>, QueryContainer>>? filter,
          [Optional] List<Func<QueryContainerDescriptor<ECommerce>, QueryContainer>>? must,
          [Optional] List<Func<QueryContainerDescriptor<ECommerce>, QueryContainer>>? mustNot,
          [Optional] List<Func<QueryContainerDescriptor<ECommerce>, QueryContainer>>? should,
          Func<SortDescriptor<ECommerce>, IPromise<IList<ISort>>> selector,
          int size,
          int page);
}