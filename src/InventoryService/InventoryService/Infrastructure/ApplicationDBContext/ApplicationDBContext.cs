using InventoryService.Domain.Models;
using InventoryService.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure.ApplicationDBContext
{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options), IApplicationDBContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Product>().HasQueryFilter(u => !u.IsDeleted);
        }
    }
}
