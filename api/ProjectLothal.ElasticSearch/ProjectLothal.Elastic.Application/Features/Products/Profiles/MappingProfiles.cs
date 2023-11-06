

using AutoMapper;
using ProjectLothal.Elastic.Application.Features.Products.Index.Create;
using ProjectLothal.ElasticSearch.Domain.Models;

namespace ProjectLothal.Elastic.Application.Features.Products.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, CreateProductResponseDto>().ReverseMap();
    }

}
