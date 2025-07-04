using ShoppingCartService.Domain.Models;

namespace ShoppingCartService.Domain.Repositories;

public interface IPurchaseOrderRepository
{
    public Task<PurchaseOrder> GetByUserIdAsync(int id);
    
    public Task CreateAsync(PurchaseOrder order);
    
    public Task UpdateAsync(string id, PurchaseOrder order);
    
    /// <summary>
    /// Inserts a purchase order if it does not exists, or updates is otherwise.
    /// </summary>
    /// <returns>True if the purchase order was inserted, false otherwise.</returns>
    public Task<bool> UpsertAsync(PurchaseOrder order);
    
    public Task DeleteAsync(string id);
}