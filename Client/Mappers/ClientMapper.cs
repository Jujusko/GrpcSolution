using AutoMapper;
using Client.Mappers.MapHelper;
using Client.Models;
using GrpcSolution.Product.V1;

namespace Client.Mappers
{
    public class ClientMapper : Profile
    {
        public ClientMapper()
        {
            CreateMap<ProductModel, GetProductByIdServiceResponse>()
                .ForMember(dst => dst.Cost, 
                    opt => opt.MapFrom(src => src.Cost.FromDecimal()))
                .ForMember(dst => dst.ProductName, 
                opt => opt.MapFrom(src => src.Name));
            CreateMap<ProductInfo, ProductModel> ()
                .ForMember(dst => dst.Id, 
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name,
                opt => opt.
                    MapFrom(src => src.ProductName))
                .ForMember(dst => dst.Cost, 
                    opt => opt.
                        MapFrom(src => src.Cost.FromProtoDecimal()));

            CreateMap<GetProductByIdServiceResponse, ProductModel>()
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.Cost, opt => 
                    opt.MapFrom(src => src.Cost.FromProtoDecimal()))
                .ForMember(dst => dst.Name,
                    opt => opt.MapFrom(src => src.ProductName));


            CreateMap<ProductModel, AddProductServiceRequest>()
                .ForMember(dst => dst.Name, 
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Cost,
                opt => opt.MapFrom(src => src.Cost.FromDecimal()));

            CreateMap<AddProductServiceResponse, ProductModel>()
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
