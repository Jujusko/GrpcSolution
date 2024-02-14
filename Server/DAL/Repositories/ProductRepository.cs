using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Server.DAL.Entities;
using Server.MapperProfiles;

namespace Server.DAL.Repositories;

internal class ProductRepository : IProductRepository
{
    private readonly IMapper _mapper;
    private readonly ILogger<ProductRepository> _logger;
    private readonly IServiceProvider _services;

    public ProductRepository(IHostEnvironment env, IServiceProvider services, 
        ILogger<ProductRepository> logger)
    {
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

        _services = services;
        _mapper = new Mapper(config);
    }

    public async Task<Product?> GetProductById(long id, CancellationToken token)
    {
        
        await using var scope = _services.CreateAsyncScope();
        await using var applicationContext = scope.ServiceProvider.GetRequiredService<TestTaskDbContext>();

        var productDto = await applicationContext.Products
            .FirstOrDefaultAsync(product => product.Id == id, token);
        if (productDto == null)
        {
            _logger.LogError("Сущности с Id {ProductId} не существует", id);
        }

        return productDto;
    }

    public async Task<long> AddProduct(Product newProduct, CancellationToken token)
    {
        await using var scope = _services.CreateAsyncScope();
        await using var applicationContext = scope.ServiceProvider.GetRequiredService<TestTaskDbContext>();
        
        if (string.IsNullOrEmpty(newProduct.Name))
        {
            _logger.LogError("Нет наименования товара, сущность не может быть создана.");
        }

        var addResult  = await applicationContext.Products.AddAsync(newProduct, token);
        var result = addResult.Entity.Id;

        return result;
    }

    public async Task<List<Product>?> GetAllProducts(int from, int amount, CancellationToken token)
    {
        await using var scope = _services.CreateAsyncScope();
        await using var applicationContext = scope.ServiceProvider.GetRequiredService<TestTaskDbContext>();

        var result = await applicationContext
            .Products.Skip(from).Take(amount).ToListAsync(token);

        return result;
    }
}