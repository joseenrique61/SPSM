using Microsoft.AspNetCore.Http.HttpResults;
using NotificationService.Application.Interfaces;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Runtime;

namespace NotificationService.Infraestructure.NotificationsProvider
{
    public class EmailProvider : INotificationProvider
    {
        private readonly ILogger<EmailProvider> _logger;
        private readonly SmtpClient smtpClient;

        private readonly string addressFrom;
        private readonly string displayName;

        public string ProviderType => "email";

        public EmailProvider(ILogger<EmailProvider> logger, IConfiguration configuration)
        {
            _logger = logger;

            addressFrom = configuration["EmailSettings:From"] ?? "";
            displayName = configuration["EmailSettings:DisplayName"] ?? "";

            // The password and host value is only used in the Email Provider Constructor
            string host = configuration["EmailSettings:Host"] ?? "";
            string port = configuration["EmailSettings:Port"] ?? "";
            string password = configuration["EmailSettings:Password"] ?? "";
            

            if (string.IsNullOrEmpty(addressFrom) || string.IsNullOrEmpty(displayName) || !int.TryParse(port, out int portNumber))
            {
                _logger.LogWarning("The EmailProvider From or DisplayName or Port for EmailProvider is not configured in .env.");
                throw new Exception("The EmailProvider From or DisplayName or Port for EmailProvider is not configured in .env.");
            }

            smtpClient = new SmtpClient
            {
                Host = host,
                Port = portNumber,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(addressFrom,password)
            };
        }

        public async Task<bool> SendAsync(string recipient, string subject, string body)
        {
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress(addressFrom, displayName);
                message.To.Add(recipient);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                
                await smtpClient.SendMailAsync(message);

                _logger.LogInformation($"Message sent to {recipient} via: {ProviderType}, header: {subject}, message: {body}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error trying to send an EMAIL to {Recipient}.", recipient);

                return false;
            }
        }
    }
}
