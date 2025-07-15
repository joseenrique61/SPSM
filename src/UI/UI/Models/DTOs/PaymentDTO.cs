namespace UI.Models.DTOs;

public class PaymentDTO
{
    public int UserId { get; set; }
    
    public List<ProductDTO> Products { get; set; } = [];
    
    public double Total { get; set; }
}