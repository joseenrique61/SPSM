using PaymentService.Domain.Models;

namespace PaymentService.Domain.Repositories;

public interface IPaymentRepository
{
    public Task RegisterPaymentAsync(PurchaseOrder purchaseOrder);
    
    public Task<List<PurchaseOrder>> GetByUserIdAsync(int id);
}