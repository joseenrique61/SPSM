using System.ComponentModel.DataAnnotations;

namespace PaymentService.Domain.Models;

public class ShippingAddress
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public required string Address { get; set; }

    [Required]
    [MaxLength(50)]
    public required string City { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Country { get; set; }
    
}