using AutoMapper;
using Client.Extensions;
using Client.Mappers;
using Client.Models;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcSolution.Product.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ILogger = Grpc.Core.Logging.ILogger;

namespace Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;


        public ProductController(IHostEnvironment env, ILogger<ProductController> logger)
        {
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
        }


        [AllowAnonymous]
        [HttpGet("All")]
        public async Task<ActionResult<List<ProductModel>>> GetAllProducts(int from, int amount)
        {
            if (from < 0 || amount < 0)
                return BadRequest("Значения не могут быть отрицательными");
            try
            {
                using var channel = GrpcChannel.ForAddress("http://localhost:5292");
                var client = new ProductService.ProductServiceClient(channel);
                var res = await client.GetAllProductsAsync(new GetAllProductsRequest
                {
                    From = from,
                    Amount = amount
                });

                var testEntity = res.Products[0];
                var mappedProduct = _mapper.Map<List<ProductModel>>(res.Products);

                return Ok(mappedProduct);
            }
            catch (RpcException e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
        
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<ProductModel> GetProduct(int id)
        {

            try
            {
                using var channel = GrpcChannel.ForAddress("http://localhost:5292");
                var client = new ProductService.ProductServiceClient(channel);
                var res = client.GetProductById(new GetProductByIdRequest
                {
                    Id = id
                });

                var mappedProduct = _mapper.Map<ProductModel>(res);

                return Ok(mappedProduct);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<int>> AddProduct(ProductModel product)
        {
            
            try
            {
                using var channel = GrpcChannel.ForAddress("http://localhost:5292");
                var client = new ProductService.ProductServiceClient(channel);
                var res =  await client.AddProductAsync(new AddProductRequest()
                {
                    Name = product.Name,
                    Cost = product.Cost.FromDecimal()
                });
                return Ok(res.Id);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
