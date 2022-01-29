namespace NotificationLib.Message
{
    public record IosMessage(
        string PushToken,
        string Alert,
        int Priority,
        bool IsBackground);
}