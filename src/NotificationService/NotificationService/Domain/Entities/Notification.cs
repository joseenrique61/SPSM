﻿using NotificationService.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace NotificationService.Domain.Entities
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public NotificationType Type { get; set; }

        [MaxLength(50)]
        public string Recipient { get; set; }
        public string? Subject { get; set; }
        public string Body { get; set; }
        public string? FailureReason { get; set; }
        public NotificationStatus Status { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public DateTime CreatedAt { get; private set; }
    }
}
