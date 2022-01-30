using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotificationLib;
using NotificationLib.Notification;
using NotificationLib.Repository;

namespace NotificationService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GenericNotificationController<TMessageType> : Controller
        where TMessageType : class
    {
        private readonly INotificationSender<TMessageType> _sender;
        private readonly INotificationStatusRepository _notificationStatusRepository;

        public GenericNotificationController(INotificationSender<TMessageType> sender,
            INotificationStatusRepository notificationStatusRepository)
        {
            _sender = sender;
            _notificationStatusRepository = notificationStatusRepository;
        }
        
        [HttpPost("send")]
        public async Task<IActionResult> SendAsync([FromBody] TMessageType message, CancellationToken token)
        {
            var deliveryResult = await _sender.SendAsync(message, token);
            await _notificationStatusRepository.SaveNotificationStatusAsync(deliveryResult, token);
            return Ok(deliveryResult.NotificationId.ToString("D"));
        }
    }
}