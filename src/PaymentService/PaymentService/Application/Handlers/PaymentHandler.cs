using PaymentService.Application.Clients;
using PaymentService.Domain.Models;
using PaymentService.Domain.Repositories;
using PaymentService.Infrastructure.Interfaces.Producers;

namespace PaymentService.Application.Handlers;

public class PaymentHandler(ILogger<PaymentHandler> logger, IPaymentRepository paymentRepository, IShoppingCartClient shoppingCartClient, IInventoryClient inventoryClient, ICustomerClient customerClient, IProducer producer) : IPaymentHandler
{
    public async Task PayAsync(PurchaseOrder purchaseOrder)
    {
        logger.LogInformation($"Processing payment for user {purchaseOrder.UserId}");
        
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

        var client = await customerClient.GetClient(purchaseOrder.UserId);


        await producer.PublishAsync(client, "payment.exchange", "notification.payment.confirmed");
    }

    public async Task<List<PurchaseOrder>> GetByUserIdAsync(int id)
    {
        return await paymentRepository.GetByUserIdAsync(id);
    }
}