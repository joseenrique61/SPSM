using InventoryService.Domain.Models;

namespace InventoryService.Domain.Repositories
{
    public interface IProductRepository
    {
        //public Task<Product?> GetProductById(int id);
        //public Task<List<Product>?> GetAllProducts();
        //public Task<List<Product>?> GetProductByName(string name);
        //public Task<List<Product>?> GetProductByCategory(string category);

        public Task<bool> SaveProductAsync(Product product);
        public Task<bool> UpdateProductAsync(Product product);
        public Task<bool> DeleteProductAsync(int id);
    }
}
