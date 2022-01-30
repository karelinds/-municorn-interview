using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using NotificationLib.Message;
using NotificationService.Controllers;

namespace NotificationService.ControllerFactory
{
    public class GenericNotificationControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private readonly List<Type> messageTypes = new List<Type>
        {
            typeof(IosMessage),
            typeof(AndroidMessage)
        };
        
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            foreach (var typeInfo in messageTypes)
            {
                Type[] typeArgs = { typeInfo };
                var controllerType = typeof(GenericNotificationController<>).MakeGenericType(typeArgs).GetTypeInfo();
                feature.Controllers.Add(controllerType);
            }
        }
    }
}