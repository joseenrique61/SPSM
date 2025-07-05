using InventoryService.Application.Interfaces;
using InventoryService.Domain.Models;

namespace InventoryService.Application.Services
{
    public class CategoryService : ICategoryService
    {
        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
