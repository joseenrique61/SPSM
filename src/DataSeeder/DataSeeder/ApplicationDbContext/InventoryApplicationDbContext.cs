using DataSeeder.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace DataSeeder.ApplicationDbContext;

public class InventoryApplicationDbContext(DbContextOptions<InventoryApplicationDbContext> options) : DbContext(options), IProductDbContext
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }
}