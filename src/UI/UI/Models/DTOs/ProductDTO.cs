namespace UI.Models.DTOs;

public class ProductDTO
{
    public required int Id { get; set; }
    
    public double Price { get; set; }
    
    public required int Amount { get; set; }
}