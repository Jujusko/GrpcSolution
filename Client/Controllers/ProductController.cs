using AutoMapper;
using Client.Extensions;
using Client.Mappers;
using Client.Models;
using Client.ServiceInterfaces;
using Grpc.Core;
using GrpcSolution.Product.V1;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
[ProducesResponseType(StatusCodes.Status504GatewayTimeout)]
public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly IMapper _mapper;
    private readonly IServer _server;

    public ProductController(IHostEnvironment env, ILogger<ProductController> logger, IServer server)
    {
        _logger = logger;
        _server = server;
        var cfg = new MapperConfiguration(c =>
        {
            c.AllowNullCollections = true;
            c.AddProfile(typeof(ProductProfile));
        });

        _mapper = new Mapper(cfg);

        if (env.IsDevelopment()) cfg.CompileMappings();
        cfg.AssertConfigurationIsValid();
    }


    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [Produces("application/json")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllProducts(int from, int amount)
    {
        if (from < 0 || amount < 0)
            return BadRequest("Значения не могут быть отрицательными");

        var result = new List<ProductModel>();


        try
        {
            var query = await _server.GrpcClient.GetAllProductsAsync(new GetAllProductsRequest
            {
                From = from,
                Amount = amount
            }, cancellationToken: HttpContext.RequestAborted);
            var list = _mapper.Map<List<ProductModel>>(query.Products);
            if (list is not null)
                result.AddRange(list);
        }
        catch (Exception e)
        {
            _logger.LogError("An error was occured  | Exception {Exception} | InnerException {InnerException}",
                e.Message, e.InnerException?.Message);
        }

        return Json(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductModel))]
    [Produces("application/json")]
    [HttpGet]
    public async Task<ActionResult<ProductModel>> GetProduct(long id)
    {
        var result = new ProductModel();
        
        try
        {
            var query = await _server.GrpcClient.GetProductByIdAsync(new GetProductByIdRequest()
            {
                Id = id
            }, cancellationToken: HttpContext.RequestAborted);
            result = _mapper.Map<ProductModel>(query);

            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError("При обработке запроса возникла ошибка | Exception {Exception} | InnerException {InnerException}",
                e.Message, e.InnerException?.Message);
        }
        
        return Json(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPost]
    public async Task<ActionResult<long>> AddProduct(ProductModel product)
    {
        long result = 0;
        try
        {
            var query = await _server.GrpcClient.AddProductAsync(new AddProductRequest()
            {
                Name = product.Name,
                Cost = product.Cost.FromDecimal()
            }, cancellationToken: HttpContext.RequestAborted);
            if (query.Id is null)
                throw new Exception("Продукт не был добавлен. Ошибка сервера.");
        }
        catch (Exception e)
        {
            _logger.LogError("При обработке запроса возникла ошибка | Exception {Exception} | InnerException {InnerException}",
                e.Message, e.InnerException?.Message);
            return Json(e.Message);
        }

        return Json(result);
    }
}