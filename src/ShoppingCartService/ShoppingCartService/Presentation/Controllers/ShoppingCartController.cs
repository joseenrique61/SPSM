using Microsoft.AspNetCore.Mvc;
using ShoppingCartService.Application;
using ShoppingCartService.Domain.Models;

namespace ShoppingCartService.Presentation.Controllers;

[Route("shopping_cart")]
[ApiController]
public class ShoppingCartController(IPurchaseOrderHandler purchaseOrderHandler) : Controller
{
    [HttpGet]
    [Route("userId/{userId}")]
    public async Task<IActionResult> GetCart(int userId)
    {
        return Ok(await purchaseOrderHandler.GetCart(userId));
    }
    
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
    
    [HttpDelete]
    [Route("delete_item/{userId}")]
    public async Task<IActionResult> DeleteItem(int userId, [FromQuery] int productId)
    { 
        return await purchaseOrderHandler.DeleteProductFromCart(userId, productId) ? Ok() : NotFound("The product was not found in the cart.");
    }

    [HttpDelete]
    [Route("clear_cart/{userId}")]
    public async Task<IActionResult> ClearCart(int userId)
    {
        await purchaseOrderHandler.ClearCart(userId);
        return Ok();
    }
}