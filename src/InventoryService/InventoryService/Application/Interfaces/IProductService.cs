using InventoryService.Application.DTOs;

namespace InventoryService.Application.Interfaces
{
    public interface IProductService
    {
        Task<bool> AddProductAsync(ProductDTO productDTO);
        Task<bool> UpdateProductAsync(int id, ProductDTO productDTO);
        Task<bool> ReduceStockAsync(List<ReduceDTO> reduceDTOs);
        Task<bool> DeleteProductAsync(int id);
    }
}
