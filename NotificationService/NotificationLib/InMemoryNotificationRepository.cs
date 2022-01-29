using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using NotificationLib.Notification;

namespace NotificationLib
{
    public class InMemoryNotificationRepository : INotificationStatusRepository
    {
        private readonly ConcurrentDictionary<Guid, DeliveryResult> _deliveryResults = new();
        
        public Task SaveNotificationStatusAsync(DeliveryResult deliveryResult,
            CancellationToken cancellationToken = default)
        {
            _deliveryResults.TryAdd(deliveryResult.NotificationId, deliveryResult);
            return Task.CompletedTask;
        }

        public Task<DeliveryResult> FindNotificationStatusAsync(Guid notificationId,
            CancellationToken cancellationToken = default) =>
                _deliveryResults.TryGetValue(notificationId, out var result) 
                    ? Task.FromResult(result) 
                    : Task.FromResult((DeliveryResult)null);
    }
}