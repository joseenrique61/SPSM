using Microsoft.AspNetCore.Mvc;
using ShoppingCartService.Application;
using ShoppingCartService.Domain.Models;

namespace ShoppingCartService.Presentation.Controllers;

[Route("shopping_cart")]
[ApiController]
public class ShoppingCartController(IPurchaseOrderHandler purchaseOrderHandler) : Controller
{
    [HttpPost]
    [Route("add_item/{userId}")]
    public async Task<IActionResult> AddItem(int userId, [FromBody] Product product)
    {
        if (product.Amount <= 0)
        {
            return BadRequest("Amount must be greater than 0.");
        }
        await purchaseOrderHandler.AddProductToCart(userId, product);
        return Ok();
    }
    
    [HttpPost]
    [Route("remove_item/{userId}")]
    public async Task<IActionResult> RemoveItem(int userId, [FromBody] Product product)
    {
        if (product.Amount <= 0)
        {
            return BadRequest("The amount must be greater than 0.");
        }
        return await purchaseOrderHandler.RemoveProductFromCart(userId, product) ? Ok() : BadRequest("The amount must be less than the amount in the cart.");
    }
}