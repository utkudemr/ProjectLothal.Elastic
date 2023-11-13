

using MassTransit.Caching.Internals;
using Nest;
using ProjectLothal.Elastic.Application.Services;
using ProjectLothal.Elastic.Core.Paging;
using ProjectLothal.ElasticSearch.Domain.Models;
using ProjectLothal.ElasticSearch.Domain.Models.ECommerceModel;
using System;
using System.Collections.Immutable;
using ECommerceModel = ProjectLothal.ElasticSearch.Domain.Models.ECommerceModel;

namespace ProjectLothal.Elastic.Persistance.Repositories;

public class ECommerceRepository : IECommerceRepository
{
    private readonly ElasticClient _client;
    private const string IndexName = "kibana_sample_data_ecommerce";

    public ECommerceRepository(ElasticClient client)
    {
        _client = client;
    }

    public async Task<Paginate<ECommerce>?> TermQuery(

        List<Func<QueryContainerDescriptor<ECommerce>, QueryContainer>>? filter,
        List<Func<QueryContainerDescriptor<ECommerce>, QueryContainer>>? must,
        List<Func<QueryContainerDescriptor<ECommerce>, QueryContainer>>? mustNot,
        List<Func<QueryContainerDescriptor<ECommerce>, QueryContainer>>? should,
        Func<SortDescriptor<ECommerce>, IPromise<IList<ISort>>> selector,
        int size,
        int page
        )
    {
        var pageFrom = (page - 1) * size;

        var result = await _client.SearchAsync<ECommerce>(x => x.Index(IndexName)
        .Query(q => q
        .Bool(bq => bq
                .Compound<ECommerce, ECommerce>(
                        must: must,
                        mustNot: mustNot,
                        filter: filter, 
                        should: should
                        )))
        .Sort(selector)
        .Size(size)
        .From(pageFrom)
        ); ;

        if (!result.IsValid) return null;
        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;

        return result.Documents.ToPaginateAsync(page, size, result.Total);
    }


}

public static class ECommerceExtension
{
    public static BoolQueryDescriptor<TModel> Compound<TModel, TResponse>(
       this BoolQueryDescriptor<TModel> selector,
       List<Func<QueryContainerDescriptor<TModel>, QueryContainer>>? must,
       List<Func<QueryContainerDescriptor<TModel>, QueryContainer>>? mustNot,
       List<Func<QueryContainerDescriptor<TModel>, QueryContainer>>? should,
       List<Func<QueryContainerDescriptor<TModel>, QueryContainer>>? filter



        ) where TModel : class
    {
        if (must is not null)
        {
            selector.Must(must);
        }
        if (mustNot is not null)
        {
            selector.MustNot(mustNot);
        }
        if (should is not null)
        {
            selector.Should(should);
        }
        if (filter is not null)
        {
            selector.Filter(filter);
        }
        return selector;
    }
}


