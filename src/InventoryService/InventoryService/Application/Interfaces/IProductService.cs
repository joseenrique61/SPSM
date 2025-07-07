using InventoryService.Domain.Models;

namespace InventoryService.Application.Interfaces
{
    public interface IProductService
    {
        Task<bool> AddProductAsync(Product product);
        Task<bool> UpdateProductAsync(int id, Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}
