using AutoMapper;
using GrpcSolution.Product.V1;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Server.DAL.Entities;
using Server.Extensions;

namespace Server.MapperProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, GetProductByIdResponse>()
            .ForMember(dst => dst.ProductName, opt =>
                opt.MapFrom(src => src.Name))
            .ForMember(dst => dst.Cost, opt =>
                opt.MapFrom(src => src.Cost.ToDecimal()));
        CreateMap<Product, GetAllProductsResponse.Types.ProductInfo>()
            .ForMember(dst => dst.Cost, 
                opt => opt.MapFrom(src => src.Cost.ToDecimal()))
            .ForMember(dst => dst.ProductName, 
                opt => opt.MapFrom(src => src.Name));
        CreateMap<AddProductRequest, Product>()
            .ForMember(dst => dst.Name,
                opt => 
                    opt.MapFrom(src => src.Name))
            .ForMember(dst => dst.Id, 
                opt => opt.Ignore())
            .ForMember(dst => dst.Cost,
            opt => opt.MapFrom(src => src.Cost.FromDecimal()));
        CreateMap<Product, AddProductResponse>()
            .ForMember(dst => dst.Id,
            opt => 
                opt.MapFrom(src => src.Id));
    }
}