using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;

namespace NotificationService.Infraestructure.NotificationsProvider
{
    public class SMSProvider : INotificationProvider
    {
        private readonly ILogger<SMSProvider> _logger;
        private readonly string? ApiKey;

        public string ProviderType => "sms";

        public SMSProvider (ILogger<SMSProvider> logger, IConfiguration configuration)
        {
            _logger = logger;
            ApiKey = configuration["NotificationsProvider:SMSApiKey"] ?? "API_NOT_CONFIGURED";

            if (ApiKey == null)
            {
                _logger.LogWarning("The API Key for SMSProvider is not configured in secrets/appsettings.");
            }
        }

        public Task<bool> SendAsync(string recipient, string subject, string body)
        {
            try
            {
                // Here would be the actual shipping logic...

                _logger.LogInformation($"Message sent to {recipient} via: {ProviderType}, header: {subject}, message: {body}");

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error trying to send an EMAIL to {Recipient}.", recipient);

                return Task.FromResult(false);
            }
        }
    }
}
