

using AutoMapper;
using MassTransit.Mediator;
using ProjectLothal.Elastic.Application.Services;
using ProjectLothal.Elastic.Core.Consumers;
using ProjectLothal.Elastic.Core.Responses;
using ProjectLothal.ElasticSearch.Domain.Models;

namespace ProjectLothal.Elastic.Application.Features.Products.Index.Create;

public record CreateProductStatus : Request<BaseResponse<CreateProductResponseDto>>
{
    public required CreateProductDto Request { get; init; }
}

public class CreateProductConsumer : MediatorRequestHandler<CreateProductStatus, BaseResponse<CreateProductResponseDto>>, Consumers
{
    protected readonly IProductRepository _repository;
    protected IMapper _mapper;

    public CreateProductConsumer(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected override async Task<BaseResponse<CreateProductResponseDto>> Handle(CreateProductStatus request, CancellationToken cancellationToken)
    {
        var product = request.Request;
        var mappedProduct = new Product()
        {
            Name = product.Name,
            Price = product.Price,
            Stock = product.Stock,
            Feature = new ProductFeature()
            {
                Width = product.FeatureDto.Width,
                Height = product.FeatureDto.Height,
                Color = product.FeatureDto.Color,
            }
        };
        var response = await _repository.IndexAsync(mappedProduct);
        if (response == null)
        {
            return BaseResponse<CreateProductResponseDto>.ErrorResponse(false, "Kayıt esnasında hata");
        }
        var mappedResponse = _mapper.Map<Product, CreateProductResponseDto>(response);
        
        return BaseResponse<CreateProductResponseDto>.SuccessResponse(mappedResponse, true);
    }
}
