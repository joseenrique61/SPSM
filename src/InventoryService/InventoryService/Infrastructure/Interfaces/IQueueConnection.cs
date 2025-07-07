using RabbitMQ.Client;

namespace InventoryService.Infrastructure.Interfaces
{
    public interface IQueueConnection
    {
        public Task InitializeAsync();

        public IChannel? Channel { get; }
    }
}
