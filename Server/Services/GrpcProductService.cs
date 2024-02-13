
using System.Numerics;
using AutoMapper;
using Grpc.Core;
using GrpcSolution.Product.V1;
using Microsoft.EntityFrameworkCore;
using Server.DAL;
using Server.DAL.Entities;

namespace Server.Services
{
    public class GrpcProductService : ProductService.ProductServiceBase
    {
        private readonly ILogger<GrpcProductService> _logger;
        private readonly TestTaskDbContext _context;
        private readonly IMapper _mapper;
        public GrpcProductService(ILogger<GrpcProductService> logger, TestTaskDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public override async Task<GetAllProductsResponse> GetAllProductsService(GetAllProductsRequest request, ServerCallContext context)
        {
            var productsFromDatabase = await _context.Products.Skip(request.From).Take(request.Amount).ToListAsync();
            var products = _mapper.Map<List<ProductInfo>>(productsFromDatabase);
            var res = new GetAllProductsResponse() { Products = { products } };
            return res;
            // return base.GetAllProductsService(request, context);
        }

        public override async Task<AddProductServiceResponse> AddProductService(AddProductServiceRequest request, ServerCallContext context)
        {
            if (!string.IsNullOrEmpty(request.Name))
            {
                var productDto = _mapper.Map<Product>(request);
                var addedProduct = await _context.
                    AddAsync(productDto, context.CancellationToken).
                    ConfigureAwait(false);
                await _context.SaveChangesAsync();
                var newProductEntity = addedProduct.Entity;
                var result = _mapper.Map<AddProductServiceResponse>(newProductEntity);
                return result;
            }
            _logger.LogError($"Empty request in {nameof(ProductService.ProductServiceBase.AddProductService)}");
            return new AddProductServiceResponse();
        }

        public override async Task<GetProductByIdServiceResponse> GetProductByIdService(GetProductByIdServiceRequest request, ServerCallContext context)
        {
            var result = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (result != null)
            {
                var productModel = _mapper.Map<GetProductByIdServiceResponse>(result);
                return productModel;
            }

            _logger.LogError($"Сущности с Id {request.Id} не существует");
            return new GetProductByIdServiceResponse();
        }
    }
}
