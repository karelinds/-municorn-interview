using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NotificationLib;
using NotificationLib.Message;
using NotificationLib.Notification;
using NotificationService.Services;

namespace NotificationService
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddNotificationsSender(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<INotificationStatusRepository, InMemoryNotificationRepository>();
            serviceCollection.AddSingleton<INotificationSender<IosNotificationMessage>, IosNotificationSender>();
            serviceCollection.AddSingleton<INotificationSender<AndroidNotificationMessage>, AndroidNotificationSender>();
            serviceCollection.AddSingleton<INotificationService, NotificationService<IosNotificationMessage>>();
            serviceCollection.AddSingleton<INotificationService, NotificationService<AndroidNotificationMessage>>();
            serviceCollection.AddSingleton<NotificationMessageSender>();

            return serviceCollection;
        }
    }
}