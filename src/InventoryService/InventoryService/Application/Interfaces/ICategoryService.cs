using InventoryService.Domain.Models;

namespace InventoryService.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> SaveAsync(Category category);
        Task<bool> UpdateAsync(Category category);
        Task<bool> DeleteAsync(int id);
    }
}
