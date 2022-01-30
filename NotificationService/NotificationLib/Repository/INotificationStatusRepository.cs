using System;
using System.Threading;
using System.Threading.Tasks;
using NotificationLib.Notification;

namespace NotificationLib.Repository
{
    //note: мы не сохраняем само сообщение, т.к. в тз это не требуется.
    //Если же это потребудется, то сами сообщения лучше хранить в отдельном хранилище.
    public interface INotificationStatusRepository
    {
        Task SaveNotificationStatusAsync(DeliveryResult deliveryResult,
            CancellationToken cancellationToken = default);

        Task<DeliveryResult> FindNotificationStatusAsync(Guid notificationId,
            CancellationToken cancellationToken = default);
    }
}