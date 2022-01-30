using Microsoft.AspNetCore.Mvc.ApplicationModels;
using NotificationService.Controllers;

namespace NotificationService.ControllerFactory
{
    public class GenericNotificationNameConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (!controller.ControllerType.IsGenericType || 
                controller.ControllerType.GetGenericTypeDefinition() != typeof(GenericNotificationController<>))
            {
                return;
            }

            var messageType = controller.ControllerType.GenericTypeArguments[0].Name;
            controller.ControllerName = messageType; 
        }
    }
}