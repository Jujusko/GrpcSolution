using Client.ServiceInterfaces;
using Grpc.Net.Client;
using GrpcSolution.Product.V1;

namespace Client.Services;

public class GrpcServerService : IServer
{
    private readonly GrpcChannel _channel;
    private readonly ILogger<GrpcServerService> _logger;

    private ProductService.ProductServiceClient? _instance;

    public GrpcServerService(IConfiguration configuration, ILogger<GrpcServerService> logger)
    {
        _logger = logger;
        var host = configuration.GetValue<string>("Services:Server:Host");
        var port = configuration.GetValue<string>("Services:Server:Port");

        var serviceAddress = string.Concat("http://", host, ":", port);

        _channel = GrpcChannel.ForAddress(serviceAddress);
    }

    public ProductService.ProductServiceClient GrpcClient
    {
        get
        {
            if (_instance is not null) return _instance;

            _instance =
                new ProductService.ProductServiceClient(
                    _channel);
            _logger.LogDebug("Service [{Service}] instance was created", nameof(GrpcServerService));

            return _instance;
        }
    }
}