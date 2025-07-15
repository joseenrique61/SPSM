using SearchService.Domain.Models;

namespace SearchService.Domain.Repositories
{
    public interface ICategoryRepository
    {
        public Task AddCategoryAsync(Category category);
        public Task<bool> UpdateCategoryAsync(int id, Category category);
        public Task<Category?> GetByIdAsync(int id);
        public Task<List<Category>> GetAll();
        public Task<Category?> GetByNameAsync(string name);
        public Task<bool> DeleteCategoryAsync(int id);
    }
}
