using ShoppingCartService.Domain.Models;
using ShoppingCartService.Domain.Repositories;

namespace ShoppingCartService.Application;

public class PurchaseOrderHandler(IPurchaseOrderRepository purchaseOrderRepository) : IPurchaseOrderHandler
{
    public async Task AddProductToCart(int userId, Product product)
    {
        var purchaseOrder = await purchaseOrderRepository.GetByUserIdAsync(userId);

        var productInCart = purchaseOrder.Products.FirstOrDefault(p => p.Id == product.Id);
        if (productInCart != null)
        {
            productInCart.Amount += product.Amount;
        }
        else
        {
            purchaseOrder.Products.Add(product);
        }

        await purchaseOrderRepository.UpsertAsync(purchaseOrder);
    }

    public async Task<bool> RemoveProductFromCart(int userId, Product product)
    {
        var purchaseOrder = await purchaseOrderRepository.GetByUserIdAsync(userId);

        var productInCart = purchaseOrder.Products.FirstOrDefault(p => p.Id == product.Id);
        if (productInCart == null)
        {
            return false;
        }

        if (productInCart.Amount < product.Amount)
        {
            return false;
        }
        productInCart.Amount -= product.Amount;

        if (productInCart.Amount == 0)
        {
            purchaseOrder.Products.Remove(productInCart);
        }
        
        await purchaseOrderRepository.UpsertAsync(purchaseOrder);
        return true;
    }
    
    public async Task<bool> DeleteProductFromCart(int userId, int productId)
    {
        var purchaseOrder = await purchaseOrderRepository.GetByUserIdAsync(userId);

        var productInCart = purchaseOrder.Products.FirstOrDefault(p => p.Id == productId);
        if (productInCart == null)
        {
            return false;
        }
        
        purchaseOrder.Products.Remove(productInCart);
        await purchaseOrderRepository.UpsertAsync(purchaseOrder);
        return true;
    }
}