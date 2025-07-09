using InventoryService.Application.DTOs;

namespace InventoryService.Application.Interfaces
{
    public interface IProductService
    {
        Task<bool> AddProductAsync(ProductDTO productDTO);
        Task<bool> UpdateProductAsync(int id, ProductDTO productDTO);
        Task<bool> ReduceStockAsync(int id, int quantity);
        Task<bool> DeleteProductAsync(int id);
    }
}
