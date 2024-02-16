using AutoMapper;
using Client.Extensions;
using Client.Models;
using GrpcSolution.Product.V1;

namespace Client.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductModel, GetProductByIdResponse>()
                .ForMember(dst => dst.Cost, 
                    opt => opt.MapFrom(src => src.Cost.FromDecimal()))
                .ForMember(dst => dst.ProductName, 
                opt => opt.MapFrom(src => src.Name));
            CreateMap<GetAllProductsResponse.Types.ProductInfo, ProductModel> ()
                .ForMember(dst => dst.Id, 
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name,
                opt => opt.
                    MapFrom(src => src.ProductName))
                .ForMember(dst => dst.Cost, 
                    opt => opt.
                        MapFrom(src => src.Cost.FromProtoDecimal()));

            CreateMap<GetProductByIdResponse, ProductModel>()
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.Cost, opt => 
                    opt.MapFrom(src => src.Cost.FromProtoDecimal()))
                .ForMember(dst => dst.Name,
                    opt => opt.MapFrom(src => src.ProductName));


            CreateMap<ProductModel, AddProductRequest>()
                .ForMember(dst => dst.Name, 
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Cost,
                opt => opt.MapFrom(src => src.Cost.FromDecimal()));

            CreateMap<AddProductResponse, ProductModel>()
                .ForMember(dst => dst.Cost, 
                    opt => opt.Ignore())
                .ForMember(dst => dst.Name,
                    opt => opt.Ignore())
                .ForMember(dst => dst.Id,
                    opt => 
                        opt.MapFrom(src => src.Id));

            

        }
    }
}
