using RabbitMQ.Client;

namespace NotificationService.Infrastructure.Interfaces
{
    public interface IQueueConnection
    {
        Task<IChannel> CreateChannelAsync();
    }
}
