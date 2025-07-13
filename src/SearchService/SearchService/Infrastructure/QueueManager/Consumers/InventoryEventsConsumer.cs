using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SearchService.Domain.Models;
using SearchService.Domain.Repositories;
using SearchService.Infrastructure.Interfaces;
using System.Text;
using System.Text.Json;

namespace SearchService.Infrastructure.QueueManager.Consumers
{
    public class InventoryEventsConsumer : BackgroundService, IConsumer
    {
        public string QueueName => "inventory.changes";

        private readonly ILogger<InventoryEventsConsumer> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IQueueConnection _queueConnection;

        private IChannel _channel;

        public InventoryEventsConsumer(ILogger<InventoryEventsConsumer> logger, IServiceScopeFactory scopeFactory, IQueueConnection queueConnection, IProductRepository productRepository)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _queueConnection = queueConnection;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("El consumidor de eventos de inventario se está iniciando.");

            try
            {
                _channel = await _queueConnection.CreateChannelAsync();

                await _channel.QueueDeclareAsync(
                   queue: QueueName,
                   durable: true,
                   exclusive: false,
                   autoDelete: false,
                   arguments: null);

                await _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

                var consumer = new AsyncEventingBasicConsumer(_channel);

                consumer.ReceivedAsync += OnMessageReceived;

                await _channel.BasicConsumeAsync(queue: QueueName, autoAck: false, consumer: consumer);

                _logger.LogInformation("Consumer started. Waiting for messages in queue '{QueueName}'.", QueueName);

                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
            }
        }

        private async Task OnMessageReceived(object sender, BasicDeliverEventArgs eventArgs)
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var routingKey = eventArgs.RoutingKey;

            _logger.LogInformation("Message received with Routing Key: '{RoutingKey}'", routingKey);

            try
            {
                await ProcessEvent(message, routingKey);
                await _channel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                await _channel.BasicNackAsync(eventArgs.DeliveryTag, multiple: false, requeue: false);
                _logger.LogError(ex, "Error processing message with Routing Key: {RoutingKey}. Message: {Message}", routingKey, message);
            }
        }

        private async Task ProcessEvent(string message, string routingKey)
        {
            using var scope = _scopeFactory.CreateScope();

            switch (routingKey)
            {
                case "inventory.product.created":
                    {
                        var product = JsonSerializer.Deserialize<Product>(message);

                        if (product != null)
                        {
                            _logger.LogInformation("Processing creation/update for product ID: {ProductId}", product.Id);


                        }
                        break;
                    }

                case "inventory.product.updated":
                    {
                        var product = JsonSerializer.Deserialize<Product>(message);
                        
                        if (product != null)
                        {
                            _logger.LogInformation("Processing creation/update for product ID: {ProductId}", product.Id);
                        }

                        break;
                    }

                case "inventory.product.deleted":
                    {
                        var deletedProduct = JsonSerializer.Deserialize<Product>(message);
                    
                        if (deletedProduct != null)
                        {
                            _logger.LogInformation("Processing deletion for product ID: {ProductId}", deletedProduct.Id);
                        }

                        break;
                    }

                case "inventory.product.reduced":
                    {
                        var reducedProduct = JsonSerializer.Deserialize<Product>(message);
                    
                        if (reducedProduct != null)
                        {
                            _logger.LogInformation("Processing reduction stock for product ID: {ProductId}", reducedProduct.Id);
                        }
                         
                        break;
                    }

                case "inventory.category.added":
                    {
                        var categoryAdded = JsonSerializer.Deserialize<Category>(message);

                        if (categoryAdded != null)
                        {
                            _logger.LogInformation("Processing reduction stock for product ID: {ProductId}", categoryAdded.Id);

                        }

                        break;
                    }
                default:
                    _logger.LogWarning("Unrecognized Routing Key: '{RoutingKey}'. The message is ignored.", routingKey);
                    break;
            }

            await Task.Delay(100);
        }

        public override void Dispose()
        {
            _channel?.CloseAsync();
            _channel?.Dispose();
            base.Dispose();
        }
    }
}