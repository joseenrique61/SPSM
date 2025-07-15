using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentService.Domain.Models;

public class PurchaseOrder
{
    [Key]
    public int Id { get; set; }
    
    public int UserId { get; set; }
    
    public List<Product> Products { get; set; } = [];
    
    public double Total { get; set; }
}