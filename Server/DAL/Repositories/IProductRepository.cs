using Server.DAL.Entities;

namespace Server.DAL.Repositories;

internal interface IProductRepository
{
    Task<Product?> GetProductById(long id, CancellationToken token);
    Task<long> AddProduct(Product newProduct, CancellationToken token);
    Task<List<Product>?> GetAllProducts(int from, int amount, CancellationToken token);
}