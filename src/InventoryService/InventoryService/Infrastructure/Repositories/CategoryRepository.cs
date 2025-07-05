using InventoryService.Domain.Models;
using InventoryService.Domain.Repositories;
using InventoryService.Infrastructure.ApplicationDBContext;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IApplicationDBContext _applicationDBContext;
        public CategoryRepository(IApplicationDBContext applicationDBContext) 
        {
            _applicationDBContext = applicationDBContext;
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            _applicationDBContext.Categories.Add(category);
            await _applicationDBContext.SaveChangesAsync();
            
            return category;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _applicationDBContext.Categories.FindAsync(id);

            if (category == null)
                return false;

            _applicationDBContext.Categories.Remove(category);
            await _applicationDBContext.SaveChangesAsync();

            return true;
        }

        public async Task<Category?> UpdateCategoryAsync(Category category)
        {
            var existingCategory = await _applicationDBContext.Categories.FindAsync(category.Id);

            if (existingCategory == null)
                return null;

            await _applicationDBContext.Categories
                .Where(c => c.Id == existingCategory.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(c => c.Name, category.Name));

            return existingCategory;
        }
    }
}
