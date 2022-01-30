using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NotificationLib;
using NotificationLib.Message;
using NotificationLib.Notification;

namespace NotificationService.Services
{
    public class NotificationMessageSender
    {
        private readonly Dictionary<Type, INotificationService> _notificationServices;

        public NotificationMessageSender(IEnumerable<INotificationService> notificationServices)
        {
            _notificationServices = notificationServices
                .ToDictionary(x => x.GetMessageType(), x => x);
        }

        public Task<DeliveryResult> SendMessageAsync(NotificationMessage notificationMessage, Type messageType,
            CancellationToken token = default)
        {
            return _notificationServices[messageType].SendMessageAsync(notificationMessage, token);
        }
    }
}