using InventoryService.Domain.Models;
using InventoryService.Domain.Repositories;

namespace InventoryService.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public Task<bool> DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>?> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>?> GetProductByCategory(string category)
        {
            throw new NotImplementedException();
        }

        public Task<Product?> GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>?> GetProductByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
