using NotificationService.Domain.Entities;
using NotificationService.Domain.Enums;
using NotificationService.Domain.Interfaces;
using NotificationService.Application.DTOs;
using NotificationService.Application.Interfaces;
using NotificationService.Infraestructure.Persistence;

namespace NotificationService.Application.Services
{
    public class NotificationAppService
    {
        private readonly Func<string, INotificationProvider> _factoryProvider;
        private readonly INotificationRepository _repository;
        private readonly ILogger<NotificationAppService> _logger;
        private readonly ApplicationDBContext _context;

        public NotificationAppService(Func<string, INotificationProvider> factoryProvider, INotificationRepository repository, ILogger<NotificationAppService> logger, ApplicationDBContext context)
        {
            _factoryProvider = factoryProvider;
            _repository = repository;
            _logger = logger;
            _context = context;
        }

        public async Task<bool> CreateAndSendNotificationAsync(NotificationRequest request)
        {
            if (!Enum.TryParse<NotificationType>(request.Type, ignoreCase: true, out var notificationType))
            {
                _logger.LogWarning("Invalid notification type received: {NotificationType}", request.Type);
                return false;
            }

            var notification = new Notification();

            notification.Type = notificationType;
            notification.Recipient = request.Recipient!;
            notification.Subject = request.Subject;
            notification.Body = request.Body!;

            await _repository.AddAsync(notification);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Notification {notification.Id} created and pending delivery.");

            try
            {
                var provider = _factoryProvider(request.Type);

                bool success = await provider.SendAsync(request.Recipient, request.Subject, request.Body!);

                if (success) 
                {
                    MarkAsSent(notification);
                    _logger.LogInformation($"Notification {notification.Id} was successfully sent to: {request.Recipient}");
                }
                else
                {
                    MarkAsFailed(notification, "The third-party provider reported a fault.");
                    _logger.LogWarning($"Notification {notification.Id} marked as failed");
                }
            }
            catch (Exception ex)
            {
                MarkAsFailed(notification, ex.Message);
                _logger.LogError(ex, $"Exception while processing Notification {notification.Id}");
            }

            await _context.SaveChangesAsync();

            return true;
        }

        // Domain Logic. These are methods that change the state of the notifications to register them into de DataBase.
        public void MarkAsSent(Notification notification)
        {
            if (notification.Status != NotificationStatus.Pending)
                throw new InvalidOperationException("Only a pending notification can be marked as sent.");

            notification.Status = NotificationStatus.Sent;
            notification.ProcessedAt = DateTime.UtcNow;
            notification.FailureReason = null;
        }

        public void MarkAsFailed(Notification notification, string reason)
        {
            if (notification.Status != NotificationStatus.Pending)
                throw new InvalidOperationException("Only a pending notification can be marked as failed.");

            notification.Status = NotificationStatus.Failed;
            notification.ProcessedAt = DateTime.UtcNow;
            notification.FailureReason = reason;
        }
    }
}
