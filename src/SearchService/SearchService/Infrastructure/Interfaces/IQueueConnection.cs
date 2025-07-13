using RabbitMQ.Client;

namespace SearchService.Infrastructure.Interfaces
{
    public interface IQueueConnection
    {
        Task<IChannel> CreateChannelAsync();
    }
}
