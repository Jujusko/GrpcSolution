using Microsoft.EntityFrameworkCore;
using Server.DAL;
using Server.DAL.Entities;

namespace Server.Services;

public class DbTest
{
    public DbTest(IServiceProvider services)
    {
        Services = services;
    }

    private IServiceProvider Services { get; }

    public async Task<Product?> GetProduct()
    {
        await using var scope = Services.CreateAsyncScope();
        await using var applicationContext = scope.ServiceProvider.GetRequiredService<TestTaskDbContext>();

        return await applicationContext.Products.FirstOrDefaultAsync(x => x.Id == 1);
    }
}