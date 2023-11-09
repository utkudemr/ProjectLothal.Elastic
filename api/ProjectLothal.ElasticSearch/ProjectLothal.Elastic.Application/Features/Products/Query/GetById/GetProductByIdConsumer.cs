
using AutoMapper;
using MassTransit.Mediator;
using ProjectLothal.Elastic.Application.Services;
using ProjectLothal.Elastic.Core.Consumers;
using ProjectLothal.Elastic.Core.Responses;
using ProjectLothal.ElasticSearch.Domain.Models;
using System.Net;

namespace ProjectLothal.Elastic.Application.Features.Products.Query.GetById
{

    public record GetProductByIdStatus : Request<BaseResponse<GetByIdProductDto>>
    {
        public string ProductId { get; set; }
    }
    public class GetProductByIdConsumer : MediatorRequestHandler<GetProductByIdStatus, BaseResponse<GetByIdProductDto>>, Consumers
    {
        protected readonly IProductRepository _repository;
        protected IMapper _mapper;

        public GetProductByIdConsumer(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        protected override async Task<BaseResponse<GetByIdProductDto>> Handle(GetProductByIdStatus request, CancellationToken cancellationToken)
        {
            var response = await _repository.GetByIdAsync(request.ProductId);
            if (response == null)
            {
                return BaseResponse<GetByIdProductDto>.ErrorResponse(false, "Ürün bulunamadı", HttpStatusCode.NotFound);
            }
            var mappedResponse = _mapper.Map<Product, GetByIdProductDto>(response);

            return BaseResponse<GetByIdProductDto>.SuccessResponse(mappedResponse, true, HttpStatusCode.OK);
        }
    }
}
