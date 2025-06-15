using Microsoft.AspNetCore.Mvc;
using NotificationService.Application.DTOs;
using NotificationService.Application.Services;

namespace NotificationsService.Presentation.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class NotificationsController : ControllerBase
    {
        private readonly NotificationAppService _appService;
        public NotificationsController(NotificationAppService appService)
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
