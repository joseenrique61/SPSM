using InventoryService.Domain.Models;

namespace InventoryService.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> AddCategoryAsync(Category category);
        Task<bool> UpdateCategoryAsync(int id, Category category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
