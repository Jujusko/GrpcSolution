using AutoMapper;
using Client.Mappers;
using Client.Models;
using GrpcSolution.Product.V1;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Client.Services;

public class ProductServiceBl
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
            cfg.AddProfile<ClientMapper>();
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

    public async Task<long> AddNewProduct(ProductModel productToAdd)
    {
        var requestModel = _mapper.Map<AddProductRequest>(productToAdd);
        long? result = await _grpcService.AddProduct(requestModel);
        if (result == null)
        {
            throw new BadHttpRequestException("Новый продукт не был добавлен");
        }

        return result.Value;
    }

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
            Console.WriteLine(e);
            throw;
        }
        return result;
    }
}