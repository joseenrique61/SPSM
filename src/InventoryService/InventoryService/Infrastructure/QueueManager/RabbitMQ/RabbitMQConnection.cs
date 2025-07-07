using InventoryService.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace InventoryService.Infrastructure.QueueManager.RabbitMQ
{
    public class RabbitMQConnection(IOptions<RabbitMQConfiguration> options) : IQueueConnection
    {
        private readonly RabbitMQConfiguration _configuration = options.Value;
        public IChannel? Channel { get; private set; }

        public async Task InitializeAsync()
        {
            await CreateConnection();
        }

        private async Task CreateConnection()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration.HostName,
                Port = _configuration.Port,
                VirtualHost = _configuration.VirtualHost,
                UserName = _configuration.Username,
                Password = _configuration.Password
            };
            var connection = await factory.CreateConnectionAsync();
            Channel = await connection.CreateChannelAsync();
        }
    }
}
