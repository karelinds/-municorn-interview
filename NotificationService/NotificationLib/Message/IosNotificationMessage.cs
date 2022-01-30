using System.ComponentModel.DataAnnotations;

namespace NotificationLib.Message
{
    public record IosNotificationMessage(
        [Required] [StringLength(50, MinimumLength = 1)] string PushToken,
        [Required] [StringLength(2000, MinimumLength = 1)] string Alert,
        int Priority = 10,
        bool IsBackground = true) : NotificationMessage;
}