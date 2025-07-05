using InventoryService.Application.Interfaces;
using InventoryService.Domain.Models;

namespace InventoryService.Application.Services
{
    public class ProductService : IProductService
    {
        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
