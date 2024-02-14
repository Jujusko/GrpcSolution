using AutoMapper;
using Grpc.Core;
using GrpcSolution.Product.V1;
using Microsoft.EntityFrameworkCore;
using Server.DAL;
using Server.DAL.Entities;
using Server.DAL.Repositories;
using Server.MapperProfiles;

namespace Server.Services;

internal class GrpcProductService : ProductService.ProductServiceBase
{
    private readonly ILogger<GrpcProductService> _logger;
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private IServiceProvider Services { get; }
    public GrpcProductService(IHostEnvironment env, IServiceProvider services, 
        ILogger<GrpcProductService> logger, IProductRepository productRepository)
    {
        Services = services;
        _logger = logger;
        _productRepository = productRepository;
        
        
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AllowNullCollections = true;
            cfg.AllowNullDestinationValues = true;
            cfg.AddProfile(typeof(ProductProfile));
        });

        if (env.IsDevelopment())
        {
            config.CompileMappings();
            config.AssertConfigurationIsValid();
        }

        _mapper = new Mapper(config);
    }

    

    public override async Task<GetProductByIdResponse> GetProductById(GetProductByIdRequest request,
        ServerCallContext context)
    {
        var result = new GetProductByIdResponse();
        if (request.Id == null)
        {
            _logger.LogError("Значение Id {RequestId} не присвоено", request.Id);
            return result;
        }
        var queryResult = await _productRepository.GetProductById(request.Id.Value, context.CancellationToken);
        if (queryResult == null)
        {
            _logger.LogError("Сущности с Id {RequestId} не существует", request.Id);
            return result;
        }
        try
        {
            var productModel = _mapper.Map<GetProductByIdResponse>(queryResult);
            if (productModel is not null)
                return productModel;
        }
        catch (Exception e)
        {
            _logger.LogError(
                "Ошибка маппинга | Exception {Exception} [{ExceptionType}] | InnerException {InnerException}",
                e.Message, typeof(Exception), e.InnerException?.Message);
        }

        _logger.LogWarning("Ошибка маппинга");

        return result;
    }

    public override async Task<AddProductResponse> AddProduct(AddProductRequest request, ServerCallContext context)
    {
        var result = new AddProductResponse();
        await using var scope = Services.CreateAsyncScope();
        await using var applicationContext = scope.ServiceProvider.GetRequiredService<TestTaskDbContext>();

        if (string.IsNullOrEmpty(request.Name))
        {
            _logger.LogError("Нет наименования товара, сущность не была добавлена.");
            return result;
        }
        
        try
        {
            var product = _mapper.Map<Product>(request);
            if (product != null)
            {
                var queryResult = await _productRepository.AddProduct(product, context.CancellationToken);
                result.Id = queryResult;
            }
                
        }
        catch (Exception e)
        {
            _logger.LogError(
                "Ошибка маппинга | Exception {Exception} [{ExceptionType}] | InnerException {InnerException}",
                e.Message, typeof(Exception), e.InnerException?.Message);
        }
        
        return result;
    }

    public override async Task<GetAllProductsResponse> GetAllProducts(GetAllProductsRequest request, ServerCallContext context)
    {
        var result = new GetAllProductsResponse();
   
        
        if (request.From < 0 || request.Amount < 0)
        {
            _logger.LogError("Данные From и Amount должны быть больше 0. From - {request.From}, Amount - {request.Amount}"
                ,request.From, request.Amount);
        }

        try
        {

            var databaseProducts =
                await _productRepository.GetAllProducts(request.From, request.Amount, context.CancellationToken);
            var mappedProducts = _mapper.Map<List<GetAllProductsResponse.Types.ProductInfo>>(databaseProducts);
            result.Products.AddRange(mappedProducts);
        }
        catch (AutoMapperMappingException e)
        {
            _logger.LogError(
                "Ошибка маппинга | Exception {Exception} [{ExceptionType}] | InnerException {InnerException}",
                e.Message, typeof(Exception), e.InnerException?.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(
                "Ошибка | Exception {Exception} [{ExceptionType}] | InnerException {InnerException}",
                e.Message, typeof(Exception), e.InnerException?.Message);
        }
        
        return result;
    }
}