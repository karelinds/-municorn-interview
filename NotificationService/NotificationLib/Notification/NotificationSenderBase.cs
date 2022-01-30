using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace NotificationLib.Notification
{
    public abstract class NotificationSenderBase<TMessage> : INotificationSender<TMessage> 
        where TMessage : class
    {
        private const string ErrorMessage = "Задача провалена успешно";
        private readonly Random random = new();
        private readonly ILogger _logger;

        protected NotificationSenderBase(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<DeliveryResult> SendAsync(TMessage message, CancellationToken token = default)
        {
            var success = random.Next(1, 5) != 1;
            var id = Guid.NewGuid();
            var result = new DeliveryResult(
                id,
                success ? DeliveryResultType.Delivered : DeliveryResultType.Failed,
                success ? null : ErrorMessage
            );
            await Task.Delay(random.Next(500, 2000), token);
            _logger.LogInformation(success
                ? $"{SenderName} sent message: {message}"
                : $"{SenderName} failed to send message: {message}");
            return result;
        }
        
        protected abstract string SenderName { get; }
    }
}