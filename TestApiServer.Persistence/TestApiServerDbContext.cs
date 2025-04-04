
using TestApiServer.Domain;
using Microsoft.EntityFrameworkCore;

namespace TestApiServer.Persistence
{
    public class TestApiServerDbContext(DbContextOptions<TestApiServerDbContext> options):DbContext(options)
    {
        public DbSet<Product> Products {  get; set; }
        public DbSet<ProductCategory> ProductCategories {  get; set; }
    }
}
