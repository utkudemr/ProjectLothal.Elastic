

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
        List<Func<QueryContainerDescriptor<ECommerce>, QueryContainer>> filter,
        Func<SortDescriptor<ECommerce>, IPromise<IList<ISort>>> selector,
        int size,
        int page

        )
    {
        var pageFrom = (page - 1) * size;

        var result = await _client.SearchAsync<ECommerce>(x => x.Index(IndexName)
        .Query(q => q
        .Bool(bq => bq.Filter(filter)))
        .Sort(selector)
        .Size(size)
        .From(pageFrom)
        );

        if (!result.IsValid) return null;
        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
     
        return result.Documents.ToPaginateAsync(page,size, result.Total);
    }
}



