using AutoMapper;
using Grpc.Core;
using GrpcSolution.Product.V1;
using Microsoft.EntityFrameworkCore;
using Server.DAL;
using Server.MapperProfiles;

namespace Server.Services;

public class GrpcProductService : ProductService.ProductServiceBase
{
    private readonly ILogger<GrpcProductService> _logger;
    private readonly IMapper _mapper;

    public GrpcProductService(IHostEnvironment env, IServiceProvider services, ILogger<GrpcProductService> logger)
    {
        Services = services;
        _logger = logger;

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

    private IServiceProvider Services { get; }

    public override async Task<GetProductByIdResponse> GetProductById(GetProductByIdRequest request,
        ServerCallContext context)
    {
        var result = new GetProductByIdResponse();

        await using var scope = Services.CreateAsyncScope();
        await using var applicationContext = scope.ServiceProvider.GetRequiredService<TestTaskDbContext>();

        var queryResult = await applicationContext.Products.FirstOrDefaultAsync(x => x.Id == request.Id);

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

    // public override async Task<GetAllProductsResponse> GetAllProductsService(GetAllProductsRequest request, ServerCallContext context)
    // {
    //     var productsFromDatabase = await _context.Products.Skip(request.From).Take(request.Amount).ToListAsync();
    //     var products = _mapper.Map<List<ProductInfo>>(productsFromDatabase);
    //     var res = new GetAllProductsResponse() { Products = { products } };
    //     return res;
    //     // return base.GetAllProductsService(request, context);
    // }
    //
    // public override async Task<AddProductServiceResponse> AddProductService(AddProductServiceRequest request, ServerCallContext context)
    // {
    //     if (!string.IsNullOrEmpty(request.Name))
    //     {
    //         var productDto = _mapper.Map<Product>(request);
    //         var addedProduct = await _context.
    //             AddAsync(productDto, context.CancellationToken).
    //             ConfigureAwait(false);
    //         await _context.SaveChangesAsync();
    //         var newProductEntity = addedProduct.Entity;
    //         var result = _mapper.Map<AddProductServiceResponse>(newProductEntity);
    //         return result;
    //     }
    //     _logger.LogError($"Empty request in {nameof(ProductService.ProductServiceBase.AddProductService)}");
    //     return new AddProductServiceResponse();
    // }
}