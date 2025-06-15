using NotificationService.Domain.Enums;

namespace NotificationService.Domain.Entities
{
    public class Notification
    {
        public Guid Id { get; private set; }
        public NotificationType Type { get; private set; }
        public string Recipient { get; private set; }
        public string? Subject { get; private set; }
        public string Body { get; private set; }
        public string? FailureReason { get; set; }
        public NotificationStatus Status { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public DateTime CreatedAt { get; private set; }

        // Private constructor so it can only be created via the static factory.
        private Notification(Guid id, NotificationType type, string recipient, string? subject, string body)
        {
            // Validation of business rules (invariants)
            if (string.IsNullOrWhiteSpace(recipient))
                throw new ArgumentException("The recipient cannot be empty.", nameof(recipient));
            if (string.IsNullOrWhiteSpace(body))
                throw new ArgumentException("The body cannot be empty.", nameof(body));
            if (type == NotificationType.Email && string.IsNullOrWhiteSpace(subject))
                throw new ArgumentException("The subject is required for email notifications.", nameof(subject));

            Id = id;
            Type = type;
            Recipient = recipient;
            Subject = subject;
            Body = body;
            Status = NotificationStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        // Factory Method. This is how a new notification is created.
        public static Notification Create(NotificationType type, string recipient, string? subject, string body)
        {
            return new Notification(Guid.NewGuid(), type, recipient, subject, body);
        }
    }
}
