using FluentValidation;

namespace NotificationLib.Message
{
    // ReSharper disable once UnusedType.Global
    public class AndroidMessageValidator : AbstractValidator<AndroidNotificationMessage>
    {
        public AndroidMessageValidator()
        {
            RuleFor(message => message.DeviceToken)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(message => message.Message)
                .NotNull()
                .NotEmpty()
                .MaximumLength(2000);
            
            RuleFor(message => message.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255);
            
            RuleFor(message => message.Condition)
                .NotNull()
                .NotEmpty()
                .MaximumLength(2000);
        }
    }
}