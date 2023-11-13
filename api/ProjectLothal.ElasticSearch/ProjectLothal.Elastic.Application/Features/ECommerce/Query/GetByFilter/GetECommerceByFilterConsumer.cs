using AutoMapper;
using MassTransit.Mediator;
using Nest;
using ProjectLothal.Elastic.Application.Features.ECommerce.Query.GetByFilter;
using ProjectLothal.Elastic.Application.Services;
using ProjectLothal.Elastic.Core.Consumers;
using ProjectLothal.Elastic.Core.Paging;
using ProjectLothal.Elastic.Core.Responses;
using System.Net;
using ECommerceModel = ProjectLothal.ElasticSearch.Domain.Models.ECommerceModel;

namespace ProjectLothal.Elastic.Application.Features.ECommerce.Query.GetById;

public record GetECommerceByFilterStatus : Request<BaseResponse<Paginate<ECommerceModel.ECommerce>>>
{
    public required GetECommerceByFilterRequest GetECommerceByFilterRequest { get; set; }
}
public class GetECommerceByFilterConsumer : MediatorRequestHandler<GetECommerceByFilterStatus, BaseResponse<Paginate<ECommerceModel.ECommerce>>>, Consumers
{
    protected readonly IECommerceRepository _repository;
    protected IMapper _mapper;

    public GetECommerceByFilterConsumer(IECommerceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected override async Task<BaseResponse<Paginate<ECommerceModel.ECommerce>>> Handle(GetECommerceByFilterStatus status, CancellationToken cancellationToken)
    {
        var request = status.GetECommerceByFilterRequest;
        Func<SortDescriptor<ECommerceModel.ECommerce>, IPromise<IList<ISort>>>? selector = null;
        var filters = new List<Func<QueryContainerDescriptor<ECommerceModel.ECommerce>, QueryContainer>>();
        if (request is null)
        {
            filters.Add(q => q.MatchAll());
        }
        else
        {
            if (!string.IsNullOrEmpty(request?.CustomerFirstName))
            {
                filters.Add(q => q.Term(t => t.Field("customer_first_name.keyword").Value(request.CustomerFirstName)));
                //filters.Add(q => q.Wildcard(a=>a.Field("customer_first_name.keyword").Wildcard(request?.CustomerFirstName)));
                //filters.Add(q => q.Fuzzy(a=>a.Field("customer_first_name.keyword")
                //    .Value(request.CustomerFirstName)
                //    .Fuzziness(Fuzziness.Auto)));
            }
            if (request?.TaxfulTotalPriceDto != null)
            {
                filters.Add(q => q.Range(t => t.Field("taxful_total_price")
                .GreaterThan(request.TaxfulTotalPriceDto.GreaterThan)
                .LessThan(request.TaxfulTotalPriceDto.LessThan)));
                selector = a => a.Descending(a => a.TaxfulTotalPrice);
            }
            if (!string.IsNullOrEmpty(request?.CategoryName))
            {
                filters.Add(q => q.Match(a => a.Field(f => f.Category).Query(request?.CategoryName)));
            }
            if (!string.IsNullOrEmpty(request?.CustomerFullName))
            {

                filters.Add(q => q.MatchPhrasePrefix(a => a.Field(b => b.CustomerFullName).Query(request.CustomerFullName)));


            }
            //filters.Add(q => q.Match(a => a.Field(f => f.Category).Query(request?.CategoryName).Operator(Operator.And)));


            //Prefix Query last word - prefix OR another words
            // Wilhemina St. Massey
            // wilhemina(term) OR St.(term) OR Massey(Prefix)
            //filters.Add(q => q.MatchBoolPrefix(a => a.Field(q => q.CustomerFullName).Query(request?.CustomerFullName)));

            //Match Phrase Query
            //filters.Add(q=>q.MatchPhrase(a=>a.Field(x=>x.CustomerFullName).Query(request?.CustomerFullName)));

            // Match Phrase Prefix
            // 1-) Match ve Prefix query'leri compound olarak yazabiliriz.
            // filters.Add(q => q
            //.Match(a => a
            //  .Field(f => f.CustomerFullName)
            // .Query(request?.CustomerFullName) ) );

            //filters.Add(q => q
            //.Prefix(p => p.Field(f => f.CustomerFullName.Suffix("keyword")).Value(request.CustomerFullName)));
            // 2-) MatchPhrasePrefix 



        }


        var result = await _repository.TermQuery(
            should: filters,
            selector: selector,
            size: request.PageSize,
            page: request.PageIndex

        );

        if (result == null || !result.Items.Any())
        {

            return BaseResponse<Paginate<ECommerceModel.ECommerce>>.ErrorResponse(false, "İstenen filtrede çıktı yok", HttpStatusCode.NotFound);
        }

        return BaseResponse<Paginate<ECommerceModel.ECommerce>>.SuccessResponse(result, true, HttpStatusCode.OK);
    }
}