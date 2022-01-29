using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotificationLib;
using NotificationLib.Message;
using NotificationLib.Notification;

namespace NotificationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationStatusRepository _notificationStatusRepository;
        private readonly INotificationSender<IosMessage> _iosSender;

        public NotificationController(INotificationStatusRepository notificationStatusRepository,
            INotificationSender<IosMessage> iosSender)
        {
            _notificationStatusRepository = notificationStatusRepository;
            _iosSender = iosSender;
        }

        [HttpPost("/send/ios")]
        public async Task<IActionResult> SendIosAsync([FromBody] IosMessage message, CancellationToken token)
        {
            var deliveryResult = await _iosSender.SendAsync(message, token);
            await _notificationStatusRepository.SaveNotificationStatusAsync(deliveryResult, token);
            return Ok(deliveryResult.NotificationId.ToString("D"));
        }

        [HttpGet("/status/{id}")]
        public async Task<IActionResult> GetStatusAsync([FromRoute] Guid id, CancellationToken token)
        {
            var status = await _notificationStatusRepository.FindNotificationStatusAsync(id, token);
            if (status == null)
                return NotFound();
            return Ok(status.Status.ToString());
        }
        
    }
}