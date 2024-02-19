using AutoMapper;
using Client.Mappers;
using Client.Models;
using Client.ServiceInterfaces;
using GrpcSolution.Product.V1;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        //cfg.AssertConfigurationIsValid();
    }


    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductModel>))]
    [Produces("application/json")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllProducts(int from, int amount)
    {
        if (from < 0 || amount < 0)
            return BadRequest("Значения не могут быть отрицательными");

        var result = new List<ProductModel>();

        var query = await _server.GrpcClient.GetAllProductsAsync(new GetAllProductsRequest
        {
            From = from,
            Amount = amount
        }, cancellationToken: HttpContext.RequestAborted);

        try
        {
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

    // [AllowAnonymous]
    // [HttpGet]
    // public ActionResult<ProductModel> GetProduct(int id)
    // {
    //     try
    //     {
    //         using var channel = GrpcChannel.ForAddress("http://localhost:5292");
    //         var client = new ProductService.ProductServiceClient(channel);
    //         var res = client.GetProductById(new GetProductByIdRequest
    //         {
    //             Id = id
    //         });
    //
    //         var mappedProduct = _mapper.Map<ProductModel>(res);
    //
    //         return Ok(mappedProduct);
    //     }
    //     catch (RpcException e)
    //     {
    //         Console.WriteLine(e);
    //         throw;
    //     }
    // }
    //
    // [AllowAnonymous]
    // [HttpPost]
    // public async Task<ActionResult<int>> AddProduct(ProductModel product)
    // {
    //     try
    //     {
    //         using var channel = GrpcChannel.ForAddress("http://localhost:5292");
    //         var client = new ProductService.ProductServiceClient(channel);
    //         var res = await client.AddProductAsync(new AddProductRequest
    //         {
    //             Name = product.Name,
    //             Cost = product.Cost.FromDecimal()
    //         });
    //         return Ok(res.Id);
    //     }
    //     catch (RpcException e)
    //     {
    //         Console.WriteLine(e);
    //         throw;
    //     }
    // }
}