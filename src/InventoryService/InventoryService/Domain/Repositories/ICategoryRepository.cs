using InventoryService.Domain.Models;

namespace InventoryService.Domain.Repositories
{
    public interface ICategoryRepository
    {
        //public Task<Category?> GetCategoryById(int id);
        //public Task<List<Category>?> GetAllCategories(int id);
        //public Task<Category?> GetCategoryByName(string name);

        public Task<bool> AddCategoryAsync(Category category);
        public Task<bool> UpdateCategoryAsync(Category category);
        public Task<bool> DeleteCategoryAsync(int id);
    }
}
