using Microsoft.EntityFrameworkCore;
using VerticalSlice.Features.Products;

namespace VerticalSlice.Infrastructure.Persistence;

public class ProductsDbContext : DbContext
{
    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Product> Products { get; set; }
}
