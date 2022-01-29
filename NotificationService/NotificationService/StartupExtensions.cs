using Microsoft.Extensions.DependencyInjection;
using NotificationLib;
using NotificationLib.Message;
using NotificationLib.Notification;

namespace NotificationService
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddNotificationsSender(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<INotificationStatusRepository, InMemoryNotificationRepository>();
            serviceCollection.AddSingleton<INotificationSender<IosMessage>, IosNotificationSender>();
            serviceCollection.AddSingleton<INotificationSender<AndroidMessage>, AndroidNotificationSender>();

            return serviceCollection;
        }
    }
}