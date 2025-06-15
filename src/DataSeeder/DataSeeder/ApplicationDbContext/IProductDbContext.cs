using DataSeeder.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace DataSeeder.ApplicationDbContext;

public interface IProductDbContext : IApplicationDbContext
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }
}