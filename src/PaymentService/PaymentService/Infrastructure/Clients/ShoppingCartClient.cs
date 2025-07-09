using PaymentService.Application.Clients;

namespace PaymentService.Infrastructure.Clients;

public class ShoppingCartClient(HttpClient httpClient) : IShoppingCartClient
{
    public async Task<bool> ClearCart(int userId)
    {
        var result = await httpClient.DeleteAsync($"clear_cart/{userId}");
        return result.IsSuccessStatusCode;
    }
}