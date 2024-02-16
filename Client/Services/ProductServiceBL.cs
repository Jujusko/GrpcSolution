using AutoMapper;
using Client.Mappers;
using Client.Models;
using GrpcSolution.Product.V1;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Client.Services;

public class ProductServiceBl : IProductServiceBl
{
    private readonly ILogger<ProductServiceBl> _logger;
    private readonly IMapper _mapper;
    private readonly IGrpcService _grpcService;

    public ProductServiceBl(IGrpcService grpcService, IHostEnvironment env, ILogger<ProductServiceBl> logger)
    {
        _grpcService = grpcService;
        _logger = logger;

        MapperConfiguration configuration = new(cfg =>
        {
            cfg.AddProfile<ProductProfile>();
            cfg.AllowNullCollections = true;
            cfg.AllowNullDestinationValues = true;
        });
        if (env.IsDevelopment())
        {
            configuration.CompileMappings();
            configuration.AssertConfigurationIsValid();
        }

        _mapper = new Mapper(configuration);
    }

    /// <summary>
    /// Получение продукта по Id
    /// </summary>
    /// <param name="id">Id продукта</param>
    /// <returns>Сущность, конвертированную в модель ProductModel</returns>
    /// <exception cref="BadHttpRequestException">Если на вход поступило отрацительное или нулевое значение Id</exception>
    public async Task<ProductModel?> GetProductById(long id)
    {
        ProductModel? result = new();

        if (id < 1)
        {
            _logger.LogError("На вход поступило значение {id}, значение должно быть >= 1", id);
            throw new BadHttpRequestException("Id не может быть меньше 1.");
        }

        var requestModel = new GetProductByIdRequest
        {
            Id = id
        };
        var response = await _grpcService.GetProductById(requestModel);
        result = _mapper.Map<ProductModel>(response);

        return result;
    }

    /// <summary>
    /// Добавление нового продукта
    /// </summary>
    /// <param name="productToAdd">Объект модели продукта для добавления</param>
    /// <returns>Id добавленного продукта</returns>
    /// <exception cref="BadHttpRequestException">В случае, если gRPC-сервер не вернул Id продукта,
    /// значит, он не был добавлен</exception>
    public async Task<long> AddNewProduct(ProductModel productToAdd)
    {
        var requestModel = _mapper.Map<AddProductRequest>(productToAdd);
        var result = await _grpcService.AddProduct(requestModel);
        var id = result.Id;
        if (id is null or 0)
        {
            throw new BadHttpRequestException("Новый продукт не был добавлен");
        }
        return id.Value;
    }

    /// <summary>
    /// Получение списка продуктов от gRPC-сервера
    /// </summary>
    /// <param name="from">Применимо к Id продукта. Значение пропускает from элементов</param>
    /// <param name="amount">Применимо к Id продукта. Значение берёт не более amount элементов</param>
    /// <returns>Список элементов, конвертированных в ProductModel</returns>
    public async Task<List<ProductModel>?> GetAllProducts(int from, int amount)
    {
        var result = new List<ProductModel>();

        var grpcRequest = await _grpcService.GetAllProducts(from, amount);
        try
        {
            result = _mapper.Map<List<ProductModel>>(grpcRequest);
        }
        catch (AutoMapperMappingException e)
        {
            _logger.LogError(
                "Ошибка маппинга | Exception {Exception} [{ExceptionType}] | InnerException {InnerException}",
                e.Message, typeof(Exception), e.InnerException?.Message);
            throw;
        }
        return result;
    }
}