using Microsoft.AspNetCore.Mvc;
using NotificationService.Application.DTOs;
using NotificationService.Application.Services;
using NotificationService.Domain.Entities;

namespace NotificationService.Presentation.Controllers
{
    [ApiController]
    [Route("notification")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationAppService _appService;
        public NotificationController(NotificationAppService appService)
        {
            _appService = appService;
        }

        [HttpPost]
        [Route("email")]
        public async Task<ActionResult> SendEmail([FromBody] EmailRequest request)
        {
            
            var status = await _appService.CreateAndSendNotificationAsync(
                new NotificationRequest("email",request.Recipient,request.Subject,request.Body)
                );
            return Ok(status);
        }

        [HttpPost]
        [Route("sms")]
        public async Task<ActionResult> SendSMS([FromBody] SMSRequest request)
        {
            var status = await _appService.CreateAndSendNotificationAsync(
                new NotificationRequest("sms", request.Recipient, request.Body)
                );
            return Ok(status);
        }
    }
}
