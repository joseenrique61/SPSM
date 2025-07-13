using PaymentService.Application.Clients;
using PaymentService.Domain.Models;
using PaymentService.Domain.Repositories;
using PaymentService.Infrastructure.Interfaces.Producers;

namespace PaymentService.Application.Handlers;

public class PaymentHandler(IPaymentRepository paymentRepository, IShoppingCartClient shoppingCartClient, IInventoryClient inventoryClient, IProducer producer) : IPaymentHandler
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

        // Get the data from User using UserService endpoint to send an email
        var client = null;

        await producer.PublishAsync(client, "payment.exchange", "notification.payment.confirmed");
    }
}