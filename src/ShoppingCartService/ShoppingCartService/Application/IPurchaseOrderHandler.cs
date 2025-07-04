using ShoppingCartService.Domain.Models;

namespace ShoppingCartService.Application;

public interface IPurchaseOrderHandler
{
    public Task AddProductToCart(int userId, Product product);
    
    public Task RemoveProductFromCart(int userId, int productId);
}