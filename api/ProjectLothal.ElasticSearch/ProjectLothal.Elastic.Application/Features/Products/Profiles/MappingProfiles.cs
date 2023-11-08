

using AutoMapper;
using ProjectLothal.Elastic.Application.Features.Products.Index.Create;
using ProjectLothal.Elastic.Application.Features.Products.Query.GetAll;
using ProjectLothal.Elastic.Application.Features.Products.Query.GetById;
using ProjectLothal.ElasticSearch.Domain.Models;

namespace ProjectLothal.Elastic.Application.Features.Products.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, CreateProductResponseDto>()
             .ForMember(destinationMember: c => c.Id, memberOptions: opt => opt.MapFrom(c => c.Id))
             .ForMember(destinationMember: c => c.Feature, memberOptions: opt => opt.MapFrom(c => c.Feature))
             .ForMember(destinationMember: c => c.Price, memberOptions: opt => opt.MapFrom(c => c.Price))
             .ForMember(destinationMember: c => c.Name, memberOptions: opt => opt.MapFrom(c => c.Name))
             .ForMember(destinationMember: c => c.Stock, memberOptions: opt => opt.MapFrom(c => c.Stock)).ReverseMap();
        CreateMap<Product, GetAllProductDto>().ReverseMap();
        CreateMap<Product, GetByIdProductDto>().ReverseMap();
        CreateMap<ProductFeature, CreateProductFeatureDto>();
        CreateMap<ProductFeature, GetByIdProductFeatureDto>();

        
    }

}
