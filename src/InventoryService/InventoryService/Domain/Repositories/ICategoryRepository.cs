using InventoryService.Domain.Models;

namespace InventoryService.Domain.Repositories
{
    public interface ICategoryRepository
    {
        public Task<Category> AddCategoryAsync(Category category);
        public Task<Category?> UpdateCategoryAsync(Category category);
        public Task<bool> DeleteCategoryAsync(int id);
    }
}
