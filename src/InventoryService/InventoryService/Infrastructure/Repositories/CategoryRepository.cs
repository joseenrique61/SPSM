using InventoryService.Domain.Models;
using InventoryService.Domain.Repositories;

namespace InventoryService.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public Task<bool> AddCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>?> GetAllCategories(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Category?> GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Category?> GetCategoryByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
