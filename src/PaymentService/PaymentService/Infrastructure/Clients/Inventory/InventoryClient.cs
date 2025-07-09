using PaymentService.Application.Clients;
using PaymentService.Domain.Models;

namespace PaymentService.Infrastructure.Clients.Inventory;

public class InventoryClient(HttpClient httpClient) : IInventoryClient
{
    public async Task<bool> ReduceStock(List<Product> products)
    {
        var result = await httpClient.PutAsJsonAsync($"reduce/", products);
        return result.IsSuccessStatusCode;
    }
}