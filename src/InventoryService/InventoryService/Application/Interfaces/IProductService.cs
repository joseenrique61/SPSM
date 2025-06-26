using InventoryService.Domain.Models;

namespace InventoryService.Application.Interfaces
{
    public interface IProductService
    {
        Task<bool> SaveAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
    }
}
