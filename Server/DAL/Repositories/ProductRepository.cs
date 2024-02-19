using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Server.DAL.Entities;
using Server.MapperProfiles;

namespace Server.DAL.Repositories;

internal class ProductRepository : IProductRepository
{
    private readonly ILogger<ProductRepository> _logger;
    private readonly IMapper _mapper;
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

    /// <summary>
    ///     Получение информации о продукте по Id (наименование и цены, если последняя занесена)
    /// </summary>
    /// <param name="id">Идентификатор продукта</param>
    /// <param name="token">Токен от сервиса, который сделал запрос</param>
    /// <returns>
    ///     Возвращает сущность из базы данных, если продукт с заданным
    ///     Id пристуствует в базе данных
    /// </returns>
    public async Task<Product?> GetProductById(long id, CancellationToken token)
    {
        await using var scope = _services.CreateAsyncScope();
        await using var applicationContext = scope.ServiceProvider.GetRequiredService<TestTaskDbContext>();

        var productDto = await applicationContext.Products
            .FirstOrDefaultAsync(product => product.Id == id, token);
        if (productDto == null) _logger.LogError("Сущности с Id {ProductId} не существует", id);

        return productDto;
    }

    /// <summary>
    ///     Добавляет продукт в базу данных
    /// </summary>
    /// <param name="newProduct">
    ///     Сущность для добавления в базу данных
    ///     с заполненными полями Name и Cost(опционально)
    /// </param>
    /// <param name="token"></param>
    /// <returns>Id добавленного продукта, если его удалось присовить (0, если операция не удалась)</returns>
    public async Task<long> AddProduct(Product newProduct, CancellationToken token)
    {
        await using var scope = _services.CreateAsyncScope();
        await using var applicationContext = scope.ServiceProvider.GetRequiredService<TestTaskDbContext>();

        var addResult = await applicationContext.Products.AddAsync(newProduct, token);
        await applicationContext.SaveChangesAsync(token);
        var result = addResult.Entity.Id;

        return result;
    }

    /// <summary>
    ///     Возвращение всех продуктов в определённом диапазоне
    /// </summary>
    /// <param name="from">С какого продукта начинать вывод (пропускает from продуктов в таблице Products)</param>
    /// <param name="amount">Пытается получить amount продуктов</param>
    /// <returns>Коллекцию сущностей из базы данных.</returns>
    public async Task<List<Product>?> GetAllProducts(int from, int amount, CancellationToken token)
    {
        await using var scope = _services.CreateAsyncScope();
        await using var applicationContext = scope.ServiceProvider.GetRequiredService<TestTaskDbContext>();

        var result = await applicationContext
            .Products.Skip(from).Take(amount).ToListAsync(token);

        return result;
    }
}