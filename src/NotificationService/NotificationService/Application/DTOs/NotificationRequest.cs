using System.ComponentModel.DataAnnotations;

namespace NotificationService.Application.DTOs
{
    public class NotificationRequest
    {
        [Required]
        public required string Type { get; set; }
        [Required]
        public required string Recipient { get; set; }
        [Required]
        public required string Subject { get; set; }
        [Required]
        public string? Body { get; set; }
    }
}
