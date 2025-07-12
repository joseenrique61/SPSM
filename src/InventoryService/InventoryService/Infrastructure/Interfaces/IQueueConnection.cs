using RabbitMQ.Client;

namespace InventoryService.Infrastructure.Interfaces
{
    public interface IQueueConnection
    {
        Task<IChannel> CreateChannelAsync();
    }
}
