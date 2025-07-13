using RabbitMQ.Client;

namespace PaymentService.Infrastructure.Interfaces
{
    public interface IQueueConnection
    {
        Task<IChannel> CreateChannelAsync();
    }
}
