namespace PaymentService.Application.Clients;

public interface IShoppingCartClient
{
    public Task<bool> ClearCart(int userId);
}