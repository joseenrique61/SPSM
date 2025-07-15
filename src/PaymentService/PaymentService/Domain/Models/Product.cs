using System.ComponentModel.DataAnnotations;

namespace PaymentService.Domain.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public required double Price { get; set; }
    
    [Required]
    public required int Amount { get; set;}
}