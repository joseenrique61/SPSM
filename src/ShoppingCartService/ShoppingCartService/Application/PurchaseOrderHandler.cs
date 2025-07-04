using ShoppingCartService.Domain.Models;
using ShoppingCartService.Domain.Repositories;

namespace ShoppingCartService.Application;

public class PurchaseOrderHandler(IPurchaseOrderRepository purchaseOrderRepository) : IPurchaseOrderHandler
{
    public async Task AddProductToCart(int userId, Product product)
    {
        var purchaseOrderFromDb = await purchaseOrderRepository.GetByUserIdAsync(userId);
        var purchaseOrder = purchaseOrderFromDb ?? new PurchaseOrder()
        {
            UserId = userId,
        };

        var productInCart = purchaseOrder.Products.FirstOrDefault(p => p.Id == product.Id);
        if (productInCart != null)
        {
            productInCart.Amount += product.Amount;
        }
        else
        {
            purchaseOrder.Products.Add(product);
        }

        if (purchaseOrderFromDb == null)
        {
            await purchaseOrderRepository.CreateAsync(purchaseOrder);
        }
        else
        {
            await purchaseOrderRepository.UpdateAsync(purchaseOrder.Id, purchaseOrder);
        }
    }
    
    public Task RemoveProductFromCart(int userId, int productId)
    {
        throw new NotImplementedException();
    }
}