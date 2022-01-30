using FluentValidation;

namespace NotificationLib.Message
{
    // ReSharper disable once UnusedType.Global
    public class IosMessageValidator : AbstractValidator<IosNotificationMessage>
    {
        public IosMessageValidator()
        {
            RuleFor(message => message.PushToken)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(message => message.Alert)
                .NotNull()
                .NotEmpty()
                .MaximumLength(2000);
        }
    }
}