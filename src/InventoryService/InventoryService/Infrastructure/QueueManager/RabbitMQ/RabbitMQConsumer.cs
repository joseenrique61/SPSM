using InventoryService.Infrastructure.Interfaces.Consumers;

namespace InventoryService.Infrastructure.QueueManager.RabbitMQ
{
    public class RabbitMQConsumer : IConsumer
    {
        public string QueueName => throw new NotImplementedException();
    }
}
