using SearchService.Domain.Models;

namespace SearchService.Domain.Repositories;

public interface IProductRepository
{
    public Task<List<Product>> GetAll();

    public Task<Product?> GetByIdAsync(int id);

    public Task<List<Product>> GetByName(string name);

    public Task<List<Product>> GetByCategory(string category);
}