using Microsoft.AspNetCore.Mvc;
using NotificationService.Application.DTOs;
using NotificationService.Application.Services;

namespace NotificationService.Presentation.Controllers
{
    [ApiController]
    [Route("api/notification")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationAppService _appService;
        public NotificationController(NotificationAppService appService)
        {
            _appService = appService;
        }

        [HttpPost] 
        public async Task<ActionResult> SendNotification([FromBody] NotificationRequest request)
        {
            var status = await _appService.CreateAndSendNotificationAsync(request);
            return Ok(status);
        }
    }
}
