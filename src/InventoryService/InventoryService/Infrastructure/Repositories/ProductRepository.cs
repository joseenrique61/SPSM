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

        public async Task<Product> AddProductAsync(Product product)
        {
            _applicationDBContext.Products.Add(product);
            await _applicationDBContext.SaveChangesAsync();

            return product;
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

        public async Task<Product?> UpdateProductAsync(Product product)
        {
            var existingProduct = await _applicationDBContext.Products.FindAsync(product.Id);

            if (existingProduct == null)
                return null;

            await _applicationDBContext.Products
                .Where(p => p.Id == existingProduct.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(p => p.Name, product.Name)
                    .SetProperty(p => p.Description, product.Description)
                    .SetProperty(p => p.Price, product.Price)
                    .SetProperty(p => p.ImagePath, product.ImagePath));

            return existingProduct;
        }
    }
}
