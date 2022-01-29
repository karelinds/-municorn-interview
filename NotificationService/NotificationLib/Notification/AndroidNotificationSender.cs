using Microsoft.Extensions.Logging;
using NotificationLib.Message;

namespace NotificationLib.Notification
{
    public class AndroidNotificationSender : NotificationSenderBase<AndroidMessage>
    {
        public AndroidNotificationSender(ILogger<AndroidNotificationSender> logger) : base(logger)
        {
        }

        protected override string SenderName => "Android notification sender";
    }
}