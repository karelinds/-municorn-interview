using System;

namespace NotificationService.Services
{
    public class UnknownMessageTypeException : Exception
    {
        public UnknownMessageTypeException(string message)
            : base(message)
        {
            
        }
    }
}