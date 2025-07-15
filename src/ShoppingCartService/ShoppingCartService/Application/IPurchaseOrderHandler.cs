using ShoppingCartService.Domain.Models;

namespace ShoppingCartService.Application;

public interface IPurchaseOrderHandler
{
    public Task<PurchaseOrder> GetCart(int userId);
    
    public Task AddProductToCart(int userId, Product product);
    
    public Task<bool> RemoveProductFromCart(int userId, Product product);

    public Task<bool> DeleteProductFromCart(int userId, int productId);
    
    public Task ClearCart(int userId);
}