namespace NotificationService.Application.DTOs
{
    public class NotificationRequest
    {
        public string Type { get; set; }
        public string Recipient { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }

        public NotificationRequest(string type, string recipient, string body) { 
            Type = type;
            Recipient = recipient; 
            Body = body;
        }

        public NotificationRequest(string type, string recipient, string subject, string body)
        {
            Type = type;
            Recipient = recipient;
            Subject = subject;
            Body = body;
        }
    }
}
