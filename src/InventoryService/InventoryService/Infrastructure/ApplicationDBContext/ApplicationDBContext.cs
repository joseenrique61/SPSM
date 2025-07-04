using InventoryService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure.ApplicationDBContext
{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options), IApplicationDBContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
