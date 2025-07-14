using PaymentService.Domain.Models;

namespace PaymentService.Application.Clients;

public interface IInventoryClient
{
    public Task<bool> ReduceStock(List<Product> products);
}