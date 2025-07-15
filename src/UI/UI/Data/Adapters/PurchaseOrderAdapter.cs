using UI.Data.Repositories.SparePartRepository;
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

    public static async Task<PaymentDTO> ToPaymentDTO(this PurchaseOrder purchaseOrder, ISparePartRepository sparePartRepository)
    {
        var orders = new List<ProductDTO>();
        foreach (var order in purchaseOrder.Orders)
        {
            orders.Add(new ProductDTO()
            {
                Id = order.SparePartId,
                Price = (await sparePartRepository.GetById(order.SparePartId))!.Price,
                Amount = order.Amount
            });
        }

        return new PaymentDTO()
        {
            UserId = purchaseOrder.ClientId,
            Products = orders,
            Total = orders.Sum(x => x.Price * x.Amount)
        };
    }
}