using ShoppingCartService.Domain.Models;

namespace ShoppingCartService.Application;

public interface IPurchaseOrderHandler
{
    public Task AddProductToCart(int userId, Product product);
    
    public Task<bool> RemoveProductFromCart(int userId, Product product);

    public Task DeleteProductFromCart(int userId, int productId);
}