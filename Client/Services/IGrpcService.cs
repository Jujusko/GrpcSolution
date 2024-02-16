using Client.Models;
using GrpcSolution.Product.V1;

namespace Client.Services;

public interface IGrpcService
{
    Task<GetAllProductsResponse?> GetAllProducts(int from, int amount);
    Task<long?> AddProduct(AddProductRequest product);
    Task<GetProductByIdResponse?> GetProductById(GetProductByIdRequest productRequest);
}