using System.ComponentModel.DataAnnotations;

namespace NotificationService.Application.DTOs
{
    public class EmailRequest
    {
        [Required]
        public required string Recipient { get; set; }
        [Required]
        public required string Subject { get; set; }
        [Required]
        public required string Body { get; set; }
    }
}
