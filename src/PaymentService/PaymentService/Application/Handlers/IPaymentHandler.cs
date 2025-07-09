using PaymentService.Domain.Models;

namespace PaymentService.Application.Handlers;

public interface IPaymentHandler
{
    public Task PayAsync(PurchaseOrder purchaseOrder);
}