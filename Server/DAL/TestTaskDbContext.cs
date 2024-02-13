using Microsoft.EntityFrameworkCore;
using Server.DAL.Entities;

namespace Server.DAL
{
    public class TestTaskDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; } //= null!;//check
        public TestTaskDbContext(DbContextOptions<TestTaskDbContext> options) : base(options) { }

    }


}
