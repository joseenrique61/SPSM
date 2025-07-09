using InventoryService.Application.DTOs;

namespace InventoryService.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> AddCategoryAsync(CategoryDTO categoryDTO);
        Task<bool> UpdateCategoryAsync(int id, CategoryDTO categoryDTO);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
