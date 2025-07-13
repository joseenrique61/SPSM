using Microsoft.EntityFrameworkCore;
using SearchService.Domain.Models;
using SearchService.Domain.Repositories;
using SearchService.Infrastructure.ApplicationDbContext;

namespace SearchService.Infrastructure.Repositories;

public class ProductRepository(IApplicationDbContext dbContext) : IProductRepository
{
    public async Task<List<Product>> GetAll()
    {
        return await dbContext.Products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await dbContext.Products.FindAsync(id);
    }

    public async Task<List<Product>> GetByName(string name)
    {
        return await dbContext.Products
            .Include(p => p.Category)
            .Where(p => p.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();
    }

    public async Task<List<Product>> GetByCategory(string categoryName)
    {
        return await dbContext.Products
            .IgnoreAutoIncludes()
            .Include(p => p.Category)
            .Where(p => p.Category!.Name.ToLower().Contains(categoryName.ToLower()))
            .ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        await dbContext.Products.AddAsync(product);
    }

    public async Task UpdateAsync(Product product)
    {
        var existingProduct = await dbContext.Products.FindAsync(product.Id);
        if (existingProduct != null)
        {
            await dbContext.Products
                .Where(p => p.Id == product.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(p => p.Name, product.Name)
                    .SetProperty(p => p.Description, product.Description)
                    .SetProperty(p => p.Price, product.Price)
                    .SetProperty(p => p.Stock, product.Stock)
                    .SetProperty(p => p.CategoryId, product.CategoryId)
                    .SetProperty(p => p.ImagePath, product.ImagePath));
        }
    }

    public async Task DeleteAsync(int id)
    {
        var product = await dbContext.Products.FindAsync(id);

        if (product != null)
        {
            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();
        }  
    }
}