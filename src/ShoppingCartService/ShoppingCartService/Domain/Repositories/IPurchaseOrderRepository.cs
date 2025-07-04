using ShoppingCartService.Domain.Models;

namespace ShoppingCartService.Domain.Repositories;

public interface IPurchaseOrderRepository
{
    public Task<PurchaseOrder?> GetByUserIdAsync(int id);
    
    public Task CreateAsync(PurchaseOrder order);
    
    public Task UpdateAsync(string id, PurchaseOrder order);
    
    public Task DeleteAsync(string id);
}