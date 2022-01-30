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
    public class NotificationStatusController : Controller
    {
        private readonly INotificationStatusRepository _notificationStatusRepository;

        public NotificationStatusController(INotificationStatusRepository notificationStatusRepository)
        {
            _notificationStatusRepository = notificationStatusRepository;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatusAsync([FromRoute] Guid id, CancellationToken token)
        {
            var status = await _notificationStatusRepository.FindNotificationStatusAsync(id, token);
            if (status == null)
                return NotFound();
            return Ok(status);
        }
    }
}