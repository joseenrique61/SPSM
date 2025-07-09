using Microsoft.EntityFrameworkCore;
using PaymentService.Domain.Models;

namespace PaymentService.Infrastructure.ApplicationDbContext;

public interface IApplicationDbContext
{
    public DbSet<Product> Products { get; set; }
    
    public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    
    public DbSet<ShippingAddress> ShippingAddresses { get; set; }
}