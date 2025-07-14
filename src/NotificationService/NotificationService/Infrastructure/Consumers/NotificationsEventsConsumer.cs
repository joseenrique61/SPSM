using System.Text;
using System.Text.Json;
using NotificationService.Application.DTOs;
using NotificationService.Application.Services;
using NotificationService.Infrastructure.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NotificationService.Infrastructure.Consumers
{
    public class NotificationsEventsConsumer(
        ILogger<NotificationsEventsConsumer> logger,
        IQueueConnection queueConnection,
        NotificationAppService notificationAppService)
        : BackgroundService, IConsumer
    {
        public string QueueName => "notification.alerts";

        private IChannel _channel;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("El consumidor de eventos de inventario se está iniciando.");

            try
            {
                _channel = await queueConnection.CreateChannelAsync();

                await _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

                var consumer = new AsyncEventingBasicConsumer(_channel);

                consumer.ReceivedAsync += OnMessageReceived;

                await _channel.BasicConsumeAsync(queue: QueueName, autoAck: false, consumer: consumer);

                logger.LogInformation("Consumer started. Waiting for messages in queue '{QueueName}'.", QueueName);

                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error");
            }
        }

        private async Task OnMessageReceived(object sender, BasicDeliverEventArgs eventArgs)
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            try
            {
                await ProcessEvent(message);
                await _channel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                await _channel.BasicNackAsync(eventArgs.DeliveryTag, multiple: false, requeue: false);
                logger.LogError(ex, "Error processing message: {Message}", message);
            }
        }

        private async Task ProcessEvent(string message)
        {
            var user = JsonSerializer.Deserialize<UserDTO>(message)!;
            await notificationAppService.CreateAndSendNotificationAsync(new NotificationRequest(type: "email", recipient: user.Email, subject: "Compra realizada", body: $"Estimado {user.Name}, su compra ha sido realizada con éxito."));
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