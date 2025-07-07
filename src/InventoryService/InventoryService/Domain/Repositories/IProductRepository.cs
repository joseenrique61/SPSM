using InventoryService.Domain.Models;

namespace InventoryService.Domain.Repositories
{
    public interface IProductRepository
    {
        public Task AddProductAsync(Product product);
        public Task<bool> UpdateProductAsync(int id, Product product);
        public Task<Product?> GetByIdAsync(int id);
        public Task<Product?> GetByNameAsync(string name);
        public Task<bool> DeleteProductAsync(int id);
    }
}
