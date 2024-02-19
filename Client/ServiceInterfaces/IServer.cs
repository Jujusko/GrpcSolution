using GrpcSolution.Product.V1;

namespace Client.ServiceInterfaces;

public interface IServer
{
    ProductService.ProductServiceClient GrpcClient { get; }
}