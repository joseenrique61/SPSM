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
        private readonly IQueueConnection _queueConnection;

        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        private IChannel _channel;

        public InventoryEventsConsumer(ILogger<InventoryEventsConsumer> logger, IQueueConnection queueConnection, IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _logger = logger;          
            _queueConnection = queueConnection;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("The inventory event consumer is starting.");

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
            switch (routingKey)
            {
                case "inventory.product.added":
                    {
                        var product = JsonSerializer.Deserialize<Product>(message);

                        if (product != null)
                        {
                            _logger.LogInformation("Processing creation for product ID: {ProductId}", product.Id);

                            await _productRepository.AddAsync(product);

                            _logger.LogInformation("Processing creation for product ID: {ProductId} was completed succesfully", product.Id);
                        }
                        break;
                    }

                case "inventory.product.updated":
                    {
                        var product = JsonSerializer.Deserialize<Product>(message);
                        
                        if (product != null)
                        {
                            _logger.LogInformation("Processing update for product ID: {ProductId}", product.Id);

                            await _productRepository.UpdateAsync(product);

                            _logger.LogInformation("Processing update for product ID: {ProductId} was completed succesfully", product.Id);
                        }

                        break;
                    }

                case "inventory.product.deleted":
                    {
                        var deletedProduct = JsonSerializer.Deserialize<Product>(message);
                    
                        if (deletedProduct != null)
                        {
                            _logger.LogInformation("Processing deletion for product ID: {ProductId}", deletedProduct.Id);

                            await _productRepository.DeleteAsync(deletedProduct.Id);

                            _logger.LogInformation("Deletion for product ID: {ProductId} was completed succesfully", deletedProduct.Id);
                        }

                        break;
                    }

                case "inventory.product.reduced":
                    {
                        var reducedProduct = JsonSerializer.Deserialize<Product>(message);
                    
                        if (reducedProduct != null)
                        {
                            _logger.LogInformation("Processing reduction stock for product ID: {ProductId}", reducedProduct.Id);

                            await _productRepository.UpdateAsync(reducedProduct);

                            _logger.LogInformation("Reduction stock for product ID: {ProductId} was completed succesfully", reducedProduct.Id);
                        }

                        break;
                    }

                case "inventory.category.added":
                    {
                        var categoryAdded = JsonSerializer.Deserialize<Category>(message);

                        if (categoryAdded != null)
                        {
                            _logger.LogInformation("Processing reduction stock for product ID: {CategoryId}", categoryAdded.Id);

                            await _categoryRepository.AddCategoryAsync(categoryAdded);

                            _logger.LogInformation("Processing reduction stock for product ID: {CategoryId} was completed succesfully", categoryAdded.Id);
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