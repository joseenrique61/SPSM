﻿using InventoryService.Domain.Models;
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

        public async Task AddCategoryAsync(Category category)
        {
            _applicationDBContext.Categories.Add(category);
            await _applicationDBContext.SaveChangesAsync();
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

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _applicationDBContext.Categories.FindAsync(id);
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _applicationDBContext.Categories
                             .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> UpdateCategoryAsync(int id, Category category)
        {
            var existingCategory = await _applicationDBContext.Categories.FindAsync(id);

            if (existingCategory == null)
                return false;

            await _applicationDBContext.Categories
                .Where(c => c.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(c => c.Name, category.Name));

            return true;
        }
    }
}
