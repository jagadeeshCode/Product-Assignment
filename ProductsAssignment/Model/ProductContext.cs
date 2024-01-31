using Microsoft.EntityFrameworkCore;

namespace ProductsAssignment.Model
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;
    }
}
