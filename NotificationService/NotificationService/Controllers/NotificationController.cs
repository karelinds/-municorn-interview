using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NotificationLib.Message;
using NotificationService.Services;

namespace NotificationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : Controller
    {
        private readonly JsonSerializerOptions jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };
        
        private readonly NotificationMessageSender _sender;

        public NotificationController(NotificationMessageSender sender)
        {
            _sender = sender;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendAsync([FromBody] JsonElement message, CancellationToken token)
        {
            Type messageType;
            try
            {
                messageType = NotificationTypeDetector.DetectNotificationType(message);
            }
            catch (UnknownMessageTypeException e)
            {
                return BadRequest(e.Message);
            }

            //TODO: Deserialize has jsonElement overload in .net6
            var messageObject = (NotificationMessage) JsonSerializer.Deserialize(message.GetRawText(), messageType, jsonSerializerOptions);
            try
            {
                var deliveryResult = await _sender.SendMessageAsync(messageObject, messageType, token);
                return Ok(deliveryResult);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}