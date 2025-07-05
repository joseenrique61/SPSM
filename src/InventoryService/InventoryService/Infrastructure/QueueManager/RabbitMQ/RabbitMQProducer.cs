using InventoryService.Domain.Models;
using InventoryService.Infrastructure.Interfaces.Producers;

namespace InventoryService.Infrastructure.QueueManager.RabbitMQ
{
    public class RabbitMQProducer : IProducer
    {
        public Task<bool> SendCategoryCreatedAlert(Category category, string exchange, string routingKey)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendCategoryDeletedAlert(Category category, string exchange, string routingKey)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendCategoryUpdatedAlert(Category category, string exchange, string routingKey)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendProductCreatedAlert(Product product, string exchange, string routingKey)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendProductDeletedAlert(Product product, string exchange, string routingKey)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendProductUpdatedAlert(Product product, string exchange, string routingKey)
        {
            throw new NotImplementedException();
        }
    }
}
