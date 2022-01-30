using System.ComponentModel.DataAnnotations;

namespace NotificationLib.Message
{
    public record AndroidNotificationMessage(
        [Required] [StringLength(50, MinimumLength = 1)] string DeviceToken,
        [Required] [StringLength(2000, MinimumLength = 1)] string Message,
        [Required] [StringLength(255, MinimumLength = 1)] string Title,
        [StringLength(2000)] string Condition) : NotificationMessage;
}