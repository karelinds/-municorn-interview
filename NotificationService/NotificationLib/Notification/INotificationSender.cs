using System.Threading;
using System.Threading.Tasks;

namespace NotificationLib.Notification
{
    public interface INotificationSender<in TMessage>
        where TMessage : class
    {
        Task<DeliveryResult> SendAsync(TMessage message, CancellationToken token = default);
    }
}