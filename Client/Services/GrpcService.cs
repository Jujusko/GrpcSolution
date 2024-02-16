using AutoMapper;
using Client.Configurations;
using Client.Mappers;
using Client.Models;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcSolution.Product.V1;

namespace Client.Services;

public class GrpcService : IGrpcService
{
    private readonly ILogger<GrpcService> _logger;
    private readonly GrpcConfiguration _grpcConfiguration;
    private readonly IMapper _mapper;
    private readonly GrpcChannel _channel;
    public GrpcService(ILogger<GrpcService> logger, GrpcConfiguration grpcConfiguration, IHostEnvironment env)
    {
        _grpcConfiguration = grpcConfiguration;
        _logger = logger;
        
        var cfg = new MapperConfiguration(c =>
        {
                
            c.AllowNullCollections = true;
            c.AddProfile(typeof(ProductProfile));
        });
        _mapper = new Mapper(cfg);

        if (env.IsDevelopment())
        {
                
            cfg.CompileMappings();
            cfg.AssertConfigurationIsValid();
        }
        
        _channel = GrpcChannel.ForAddress($"http://{_grpcConfiguration.Url}:{_grpcConfiguration.Port}");
    }

    public async Task<GetAllProductsResponse?> GetAllProducts(int from, int amount)
    {
        GetAllProductsResponse? result = null;
        if (from < 0 || amount < 0)
            throw new Exception("Значения не могут быть отрицательными");
        try
        {
            using var channel = GrpcChannel.ForAddress($"http://{_grpcConfiguration.Url}:{_grpcConfiguration.Port}");
            var client = new ProductService.ProductServiceClient(channel);
            result = await client.GetAllProductsAsync(new GetAllProductsRequest
            {
                From = from,
                Amount = amount
            });
            return result;

        }
        catch (RpcException e)
        {
            _logger.LogError(
                "Ошибка во время выполнения gRPC запроса | Exception {Exception} [{ExceptionType}] | InnerException {InnerException}",
                e.Message, typeof(Exception), e.InnerException?.Message);
            return result;
        }
        catch (AutoMapperMappingException e)
        {
            _logger.LogError(
                "Ошибка маппинга | Exception {Exception} [{ExceptionType}] | InnerException {InnerException}",
                e.Message, typeof(Exception), e.InnerException?.Message);
            return result;
        }
    }

    /// <summary>
    /// Удалённый вызов метода на добавление нового продукта с передачей модели AddProductRequest
    /// </summary>
    /// <param name="product">Заполненная модель продукта для добавления</param>
    /// <returns>Ответ от сервера - AddProductResponse, содержит Id новой сущности</returns>
    public async Task<AddProductResponse> AddProduct(AddProductRequest product)
    {
        var client = new ProductService.ProductServiceClient(_channel);

        AddProductResponse result = new();
        
        try
        {
            result = await client.AddProductAsync(product);
        }
        catch (RpcException e)
        {
            _logger.LogError(
                "Ошибка во время выполнения запроса | Exception {Exception} [{ExceptionType}] | InnerException {InnerException}",
                e.Message, typeof(Exception), e.InnerException?.Message);
            return result;
        }
        return result;
    }


    /// <summary>
    /// Удалённый вызов метода на получение существующего продукта с передачей модели GetProductByIdRequest
    /// </summary>
    /// <param name="productRequest">Модель из прото-контракта, содержащая в себе Id продукта</param>
    /// <returns>Ответ от сервера - GetProductByIdResponse, содержит информацию о сущности</returns>
    public async Task<GetProductByIdResponse?> GetProductById(GetProductByIdRequest productRequest)
    {
        using var channel = GrpcChannel.ForAddress($"http://{_grpcConfiguration.Url}:{_grpcConfiguration.Port}");
        var client = new ProductService.ProductServiceClient(channel);

        GetProductByIdResponse result = new();//???

        try
        {
            result = await client.GetProductByIdAsync(productRequest);
            
            return result;
        }
        catch (RpcException e)
        {
            _logger.LogError(
                "Ошибка во время выполнения запроса | Exception {Exception} [{ExceptionType}] | InnerException {InnerException}",
                e.Message, typeof(Exception), e.InnerException?.Message);
            return result;
        }
    }

    ~GrpcService()
    {
        _channel?.Dispose();
    }
  
}