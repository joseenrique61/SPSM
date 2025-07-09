using PaymentService.Application.Clients;
using PaymentService.Domain.Models;

namespace PaymentService.Infrastructure.Clients.Inventory;

public class MockInventoryClient : IInventoryClient
{
    public async Task<bool> ReduceStock(List<Product> products)
    {
        return true;
    }
}