using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using Moq;
using NotificationLib;
using NotificationLib.Message;
using NotificationLib.Notification;
using NotificationLib.Repository;
using NUnit.Framework;

namespace NotificationServiceTests
{
    public class NotificationServiceTests
    {
        private INotificationService _notificationService;
        private INotificationStatusRepository _notificationRepository;
        private Mock<INotificationSender<IosNotificationMessage>> _notificationSenderMock;

        private DeliveryResult _deliveryResult = new(Guid.NewGuid(), DeliveryResultType.Delivered, null);
        
        [SetUp]
        public void SetUp()
        {
            _notificationRepository = new InMemoryNotificationRepository();
            _notificationSenderMock = new Mock<INotificationSender<IosNotificationMessage>>();
            _notificationSenderMock.Setup(
                    x => x.SendAsync(It.IsAny<IosNotificationMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_deliveryResult);
            _notificationService = new NotificationService<IosNotificationMessage>(
                _notificationRepository,
                _notificationSenderMock.Object,
                new IosMessageValidator());
        }
        
        [Test]
        public async Task SendMessage_ShouldSaveInRepository_And_CallMessageSender()
        {
            var message = new IosNotificationMessage("token", "alert");
            var deliveryResult = await _notificationService.SendMessageAsync(message);
            var deliveryResultSaved = await _notificationRepository.FindNotificationStatusAsync(deliveryResult.NotificationId);
            
            deliveryResult.Should().Be(_deliveryResult);
            deliveryResultSaved.Should().Be(_deliveryResult);
            _notificationSenderMock
                .Verify(x => x.SendAsync(message, It.IsAny<CancellationToken>()), Times.Once);
        }
        
        [Test]
        public async Task SendInvalidMessage_ShouldThrowException()
        {
            var message = new IosNotificationMessage("loooooooooooooooooooooooooooooooooooooooooooooooooooooooongToken", "alert");
            Func<Task> call = () => _notificationService.SendMessageAsync(message);
            await call.Should().ThrowAsync<ValidationException>();
        }
        
    }
}