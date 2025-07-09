using InventoryService.Domain.Models;
using InventoryService.Domain.Repositories;
using InventoryService.Infrastructure.ApplicationDBContext;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IApplicationDBContext _applicationDBContext;
        public ProductRepository(IApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public async Task AddProductAsync(Product product)
        {
            _applicationDBContext.Products.Add(product);
            await _applicationDBContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _applicationDBContext.Products.FindAsync(id);

            if (product == null)
                return false;

            _applicationDBContext.Products.Remove(product);
            await _applicationDBContext.SaveChangesAsync();

            return true;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _applicationDBContext.Products.FindAsync(id);
        }

        public async Task<Product?> GetByNameAsync(string name)
        {
            return await _applicationDBContext.Products
                             .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> UpdateProductAsync(int id, Product product)
        {
            var existingProduct = await _applicationDBContext.Products.FindAsync(id);

            if (existingProduct == null)
                return false;

            await _applicationDBContext.Products
                .Where(p => p.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(p => p.Name, product.Name)
                    .SetProperty(p => p.Description, product.Description)
                    .SetProperty(p => p.Price, product.Price)
                    .SetProperty(p => p.Stock, product.Stock)
                    .SetProperty(p => p.CategoryId, product.CategoryId)
                    .SetProperty(p => p.ImagePath, product.ImagePath));

            return true;
        }
    }
}
