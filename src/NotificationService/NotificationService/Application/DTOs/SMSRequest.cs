using System.ComponentModel.DataAnnotations;

namespace NotificationService.Application.DTOs
{
    public class SMSRequest
    {
        [Required]
        public required string Recipient { get; set; }
        [Required]
        public required string Body { get; set; }
    }
}
