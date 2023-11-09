

using Nest;
using ProjectLothal.Elastic.Application.Services;
using ProjectLothal.ElasticSearch.Domain.Models;
using System.Collections.Immutable;
using ECommerceModel = ProjectLothal.ElasticSearch.Domain.Models.ECommerceModel;

namespace ProjectLothal.Elastic.Persistance.Repositories;

public class ECommerceRepository: IECommerceRepository
{
    private readonly ElasticClient _client;
    private const string IndexName = "kibana_sample_data_ecommerce";

    public ECommerceRepository(ElasticClient client)
    {
        _client = client;
    }

    public async Task<IReadOnlyCollection<ECommerceModel.ECommerce>?> TermQuery(List<Func<QueryContainerDescriptor<ECommerceModel.ECommerce>, QueryContainer>> filter)
    {
        var result = await _client.SearchAsync<ECommerceModel.ECommerce>(x => x.Index(IndexName).Query(q => q.Bool(bq => bq.Filter(filter))));
        if (!result.IsValid) return null;
        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
        return result.Documents.ToImmutableList();
    }
}
