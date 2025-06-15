using DataSeeder.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace DataSeeder.ApplicationDbContext;

public class SearchApplicationDbContext(DbContextOptions<SearchApplicationDbContext> options) : DbContext(options), IProductDbContext
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }
}