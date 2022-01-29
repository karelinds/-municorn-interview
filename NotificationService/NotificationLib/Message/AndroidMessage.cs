namespace NotificationLib.Message
{
    public record AndroidMessage(
        string DeviceToken,
        string Message,
        string Title,
        string Condition);
}