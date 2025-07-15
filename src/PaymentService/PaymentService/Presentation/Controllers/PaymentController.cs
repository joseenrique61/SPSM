using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.Handlers;
using PaymentService.Domain.Models;

namespace PaymentService.Presentation.Controllers;

[ApiController]
public class PaymentController(IPaymentHandler paymentHandler, ILogger<PaymentController> logger) : Controller
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
            logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("payment/userId/{id}")]
    public async Task<IActionResult> GetByUserId(int id)
    {
        try
        {
            return Ok(await paymentHandler.GetByUserIdAsync(id));
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}