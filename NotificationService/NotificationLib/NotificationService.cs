using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using NotificationLib.Message;
using NotificationLib.Notification;
using NotificationLib.Repository;
using ValidationException = FluentValidation.ValidationException;

namespace NotificationLib
{
    public interface INotificationService
    {
        Task<DeliveryResult> SendMessageAsync(NotificationMessage notificationMessage, CancellationToken token = default);
        Type GetMessageType();
    }

    public class NotificationService<TMessage> : INotificationService
        where TMessage : NotificationMessage
    {
        private readonly INotificationStatusRepository _statusRepository;
        private readonly INotificationSender<TMessage> _sender;
        private readonly IValidator<TMessage> _validator;

        public NotificationService(INotificationStatusRepository statusRepository,
            INotificationSender<TMessage> sender,
            IValidator<TMessage> validator)
        {
            _statusRepository = statusRepository;
            _sender = sender;
            _validator = validator;
        }

        public async Task<DeliveryResult> SendMessageAsync(NotificationMessage notificationMessage, CancellationToken token = default)
        {
            var validationResult = await _validator.ValidateAsync((TMessage)notificationMessage, token);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var deliveryResult = await _sender.SendAsync((TMessage)notificationMessage, token);
            await _statusRepository.SaveNotificationStatusAsync(deliveryResult, token);
            return deliveryResult;
        }

        public Type GetMessageType() => typeof(TMessage);
    }
}