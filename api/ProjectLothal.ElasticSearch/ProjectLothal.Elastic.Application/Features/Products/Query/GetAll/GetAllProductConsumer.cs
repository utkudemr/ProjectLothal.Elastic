
using AutoMapper;
using MassTransit.Mediator;
using ProjectLothal.Elastic.Application.Services;
using ProjectLothal.Elastic.Core.Consumers;
using ProjectLothal.Elastic.Core.Responses;
using ProjectLothal.ElasticSearch.Domain.Models;
using System.Net;

namespace ProjectLothal.Elastic.Application.Features.Products.Query.GetAll
{

    public record GetAllProductStatus : Request<BaseResponse<IReadOnlyCollection<GetAllProductDto>>>
    {
    }
    public class GetProductByIdConsumer : MediatorRequestHandler<GetAllProductStatus, BaseResponse<IReadOnlyCollection<GetAllProductDto>>>, Consumers
    {
        protected readonly IProductRepository _repository;
        protected IMapper _mapper;

        public GetProductByIdConsumer(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        protected override async Task<BaseResponse<IReadOnlyCollection<GetAllProductDto>>> Handle(GetAllProductStatus request, CancellationToken cancellationToken)
        {
            var response = await _repository.GetAllAsync();
            if (response == null)
            {
                return BaseResponse<IReadOnlyCollection<GetAllProductDto>>.ErrorResponse(false, "Ürün listesi bulunamadı", HttpStatusCode.NotFound);
            }
            var mappedResponse = _mapper.Map<IReadOnlyCollection<Product>, IReadOnlyCollection<GetAllProductDto>>(response);

            return BaseResponse<IReadOnlyCollection<GetAllProductDto>>.SuccessResponse(mappedResponse, true, HttpStatusCode.OK);
        }
    }
}
