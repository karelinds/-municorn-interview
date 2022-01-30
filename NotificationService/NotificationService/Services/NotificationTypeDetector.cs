using System;
using System.Collections.Generic;
using System.Text.Json;
using NotificationLib.Message;

namespace NotificationService.Services
{
    public static class NotificationTypeDetector
    {
        public static Type DetectNotificationType(JsonElement jElement)
        {
            if (jElement.ValueKind != JsonValueKind.Object)
                throw new UnknownMessageTypeException($"Unknown message type. {jElement.GetRawText()}");
            
            var propertiesNames = new HashSet<string>();
            foreach (var property in jElement.EnumerateObject())
            {
                propertiesNames.Add(property.Name.ToLowerInvariant());
            }
            
            if (propertiesNames.Contains("pushtoken"))
                return typeof(IosNotificationMessage);
            if (propertiesNames.Contains("devicetoken"))
                return typeof(AndroidNotificationMessage);
            
            throw new UnknownMessageTypeException($"Unknown message type. {jElement.GetRawText()}");
        }
    }
}