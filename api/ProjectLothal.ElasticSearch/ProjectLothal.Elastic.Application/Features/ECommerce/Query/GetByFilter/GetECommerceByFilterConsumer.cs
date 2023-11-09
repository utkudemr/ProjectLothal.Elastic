

using AutoMapper;
using MassTransit.Mediator;
using Nest;
using ProjectLothal.Elastic.Application.Services;
using ProjectLothal.Elastic.Core.Consumers;
using ProjectLothal.Elastic.Core.Responses;
using System.Net;
using ECommerceModel= ProjectLothal.ElasticSearch.Domain.Models.ECommerceModel;

namespace ProjectLothal.Elastic.Application.Features.ECommerce.Query.GetById;

public record GetECommerceByFilterStatus : Request<BaseResponse<IReadOnlyCollection<ECommerceModel.ECommerce>>>
{
    public string? CustomerFirstName { get; set; }
}
public class GetECommerceByFilterConsumer : MediatorRequestHandler<GetECommerceByFilterStatus, BaseResponse<IReadOnlyCollection<ECommerceModel.ECommerce>>>, Consumers
{
    protected readonly IECommerceRepository _repository;
    protected IMapper _mapper;

    public GetECommerceByFilterConsumer(IECommerceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected override async Task<BaseResponse<IReadOnlyCollection<ECommerceModel.ECommerce>>> Handle(GetECommerceByFilterStatus request, CancellationToken cancellationToken)
    {

        var filters = new List<Func<QueryContainerDescriptor<ECommerceModel.ECommerce>, QueryContainer>>();
        if (!string.IsNullOrEmpty(request.CustomerFirstName))
        {
            filters.Add(q => q.Term(t => t.Field("customer_first_name.keyword").Value(request.CustomerFirstName)));
        }
        var result = await _repository.TermQuery(filters);

        if(result==null || !result.Any()) {

            return BaseResponse<IReadOnlyCollection<ECommerceModel.ECommerce>>.ErrorResponse( false,"İstenen filtrede çıktı yok", HttpStatusCode.NotFound);
        }

        return BaseResponse<IReadOnlyCollection<ECommerceModel.ECommerce>>.SuccessResponse(result, true, HttpStatusCode.OK);
    }
}
