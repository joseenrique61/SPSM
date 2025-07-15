using UI.Models;
using UI.Models.DTOs;

namespace UI.Data.Adapters;

public static class PurchaseOrderAdapter
{
    public static PurchaseOrder ToPurchaseOrder(this PurchaseOrderDTO dto)
    {
        var orders = dto.Products.Select(x => new Order()
        {
            SparePartId = x.Id,
            Amount = x.Amount
        });
        
        return new PurchaseOrder()
        {
            ClientId = dto.UserId,
            Orders = orders.ToList(),
            PurchaseCompleted = false
        };
    }

    public static PurchaseOrderDTO ToPurchaseOrderDTO(this PurchaseOrder purchaseOrder)
    {
        throw new NotImplementedException();
    }
}