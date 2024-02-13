using Microsoft.EntityFrameworkCore;
using Server.DAL.Entities;

namespace Server.DAL;

public class TestTaskDbContext : DbContext
{
    public TestTaskDbContext(DbContextOptions<TestTaskDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;
}