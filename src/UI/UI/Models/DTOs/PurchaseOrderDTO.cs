namespace UI.Models.DTOs;

public class PurchaseOrderDTO
{
    public int UserId { get; set; }

    public List<ProductDTO> Products { get; set; } = [];
}