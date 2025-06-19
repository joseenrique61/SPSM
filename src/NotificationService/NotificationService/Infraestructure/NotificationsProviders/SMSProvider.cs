using Microsoft.Identity.Client;
using NotificationService.Application.Interfaces;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace NotificationService.Infraestructure.NotificationsProvider
{
    public class SMSProvider : INotificationProvider
    {
        private readonly ILogger<SMSProvider> _logger;

        private readonly string twilioNumber;
        public string ProviderType => "sms";

        public SMSProvider (ILogger<SMSProvider> logger, IConfiguration configuration)
        {
            _logger = logger;

            twilioNumber = configuration["SmsSettings:TwilioNumber"] ?? "";

            string accountSid = configuration["SmsSettings:AccountSid"] ?? "";
            string authToken = configuration["SmsSettings:AuthToken"] ?? "";

            if (string.IsNullOrEmpty(accountSid) || string.IsNullOrEmpty(authToken) || string.IsNullOrEmpty(twilioNumber))
            {
                _logger.LogWarning("The AccountSId or AuthToken Key or TwilioNumber for SMSProvider  is not configured in .env.");
                throw new Exception("The AccountSId or AuthToken Key or TwilioNumber for SMSProvider  is not configured in .env.");
            }

            TwilioClient.Init(
                accountSid,
                authToken
                );
        }

        public Task<bool> SendAsync(string recipient, string? subject, string body)
        {
            try
            {
                MessageResource.Create(
                    to: new PhoneNumber(recipient),
                    from: new PhoneNumber(twilioNumber),
                    body: body);

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
