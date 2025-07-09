using Microsoft.EntityFrameworkCore;
using PaymentService.Domain.Models;

namespace PaymentService.Infrastructure.ApplicationDbContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<Product> Products { get; set; }
    
    public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    
    public DbSet<ShippingAddress> ShippingAddresses { get; set; }
}