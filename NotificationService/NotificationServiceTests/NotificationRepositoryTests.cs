using System;
using System.Threading.Tasks;
using FluentAssertions;
using NotificationLib.Notification;
using NotificationLib.Repository;
using NUnit.Framework;

namespace NotificationServiceTests
{
    public class NotificationRepositoryTests
    {
        [Test]
        public async Task SaveDeliveryResult_GetById_ShouldReturnSavedResult()
        {
            var repository = new InMemoryNotificationRepository();
            var deliveryResult = new DeliveryResult(Guid.NewGuid(), DeliveryResultType.Delivered, null);
            await repository.SaveNotificationStatusAsync(deliveryResult);
            var deliveryResultSaved = await repository.FindNotificationStatusAsync(deliveryResult.NotificationId);
            deliveryResultSaved.Should().Be(deliveryResult);
        }
        
        [Test]
        public async Task FindNonExistingDeliveryResult_ShouldReturnNull()
        {
            var repository = new InMemoryNotificationRepository();
            var deliveryResultSaved = await repository.FindNotificationStatusAsync(Guid.NewGuid());
            deliveryResultSaved.Should().BeNull();
        }
    }
}