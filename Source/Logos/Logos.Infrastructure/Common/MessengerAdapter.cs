using GalaSoft.MvvmLight.Messaging;
using System;
namespace Logos.Infrastructure.Common
{
    public class MessengerAdapter : IMessengerAdapter
    {
        public void RegisterMessageHandler<T>(object recipient, Action<T> messageHandler)
        {
            Messenger.Default.Register<T>(recipient, messageHandler);
        }

        public void Send<T>(T value)
        {
            Messenger.Default.Send<T>(value);
        }

    }
}