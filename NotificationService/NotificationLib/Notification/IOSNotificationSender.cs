using Microsoft.Extensions.Logging;
using NotificationLib.Message;

namespace NotificationLib.Notification
{
    public class IosNotificationSender : NotificationSenderBase<IosNotificationMessage>
    {
        public IosNotificationSender(ILogger<IosNotificationSender> logger) : base(logger)
        {
        }

        protected override string SenderName => "iOS notification sender";
    }
}