using Microsoft.EntityFrameworkCore;
using SearchService.Domain.Models;

namespace SearchService.Infrastructure.ApplicationDbContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }
}