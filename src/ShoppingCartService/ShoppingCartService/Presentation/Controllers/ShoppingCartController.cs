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
        await purchaseOrderHandler.AddProductToCart(userId, product);
        return Ok();
    }
}