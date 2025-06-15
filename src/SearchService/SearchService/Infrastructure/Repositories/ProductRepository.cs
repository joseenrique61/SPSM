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
}