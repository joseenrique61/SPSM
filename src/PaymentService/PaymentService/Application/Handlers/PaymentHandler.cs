using PaymentService.Application.Clients;
using PaymentService.Domain.Models;
using PaymentService.Domain.Repositories;

namespace PaymentService.Application.Handlers;

public class PaymentHandler(IPaymentRepository paymentRepository, IShoppingCartClient shoppingCartClient, IInventoryClient inventoryClient) : IPaymentHandler
{
    public async Task PayAsync(PurchaseOrder purchaseOrder)
    {
        if (!await inventoryClient.ReduceStock(purchaseOrder.Products))
        {
            throw new Exception("Not enough stock.");
        }

        if (!await shoppingCartClient.ClearCart(purchaseOrder.UserId))
        {
            throw new Exception("Error clearing cart.");
        }
        await paymentRepository.RegisterPaymentAsync(purchaseOrder);
    }
}