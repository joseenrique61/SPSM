using InventoryService.Domain.Models;

namespace InventoryService.Domain.Repositories
{
    public interface IProductRepository
    {
        public Task<Product> AddProductAsync(Product product);
        public Task<Product?> UpdateProductAsync(Product product);
        public Task<bool> DeleteProductAsync(int id);
    }
}
