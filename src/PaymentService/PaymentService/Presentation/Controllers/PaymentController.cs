using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.Handlers;
using PaymentService.Domain.Models;

namespace PaymentService.Presentation.Controllers;

[ApiController]
public class PaymentController(IPaymentHandler paymentHandler) : Controller
{
    [HttpPost]
    [Route("pay")]
    public async Task<IActionResult> Pay([FromBody] PurchaseOrder purchaseOrder)
    {
        try
        {
            await paymentHandler.PayAsync(purchaseOrder);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}