using System;

namespace NotificationLib.Notification
{
    public record DeliveryResult(
        Guid NotificationId,
        DeliveryResultType Status,
        string ErrorMessage);
}