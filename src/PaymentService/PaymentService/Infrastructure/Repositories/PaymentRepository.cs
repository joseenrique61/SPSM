using Microsoft.EntityFrameworkCore;
using PaymentService.Domain.Models;
using PaymentService.Domain.Repositories;

namespace PaymentService.Infrastructure.Repositories;

public class PaymentRepository(ApplicationDbContext.ApplicationDbContext applicationDbContext) : IPaymentRepository
{
    public async Task RegisterPaymentAsync(PurchaseOrder purchaseOrder)
    {
        await applicationDbContext.PurchaseOrders.AddAsync(purchaseOrder);
        await applicationDbContext.SaveChangesAsync();
    }

    public async Task<List<PurchaseOrder>> GetByUserIdAsync(int id)
    {
        return await applicationDbContext.PurchaseOrders
            .Include(p => p.Products)
            .Where(p => p.UserId == id)
            .ToListAsync();
    }
}