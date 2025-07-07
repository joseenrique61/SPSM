using InventoryService.Domain.Models;

namespace InventoryService.Infrastructure.Interfaces.Producers
{
    public interface IProducer
    {
        public Task<bool> SendProductCreatedAlert(Product product, string exchange, string routingKey);
        public Task<bool> SendCategoryCreatedAlert(Category category, string exchange, string routingKey);
        public Task<bool> SendProductUpdatedAlert(Product product, string exchange, string routingKey);
        public Task<bool> SendCategoryUpdatedAlert(Category category, string exchange, string routingKey);
        public Task<bool> SendProductDeletedAlert(Product product, string exchange, string routingKey);
        public Task<bool> SendCategoryDeletedAlert(Category category, string exchange, string routingKey);
    }
}
