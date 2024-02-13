using AutoMapper;
using GrpcSolution.Product.V1;
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
                opt.MapFrom(src => src.Cost.ToDecimal()))
            ;

        //ForMember and ForPath diff check
        //add for each prop
        // CreateMap<Product, GetProductByIdServiceResponse>()
        //     .ForMember(dest => dest.Cost, 
        //         opt => opt.MapFrom(src => src.Cost.FromDecimal()))
        //     .ForMember(dest => dest.ProductName,
        //     opt => opt.MapFrom(src => src.Name));
        // CreateMap<AddProductServiceRequest, Product>().ForMember(dst => dst.Cost,
        //     opt => opt.MapFrom(src => src.Cost.FromProtoDecimal()));
        // CreateMap<Product, AddProductServiceResponse>();
        //
        // CreateMap<Product, ProductInfo>()
        //     .ForMember(dst => dst.ProductName,
        //         opt => opt.MapFrom(src => src.Name))
        //     .ForMember(dst => dst.Cost,
        //         opt => opt.MapFrom(src => src.Cost.FromDecimal()));
    }
}