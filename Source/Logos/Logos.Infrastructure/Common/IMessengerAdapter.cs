using System;
namespace Logos.Infrastructure.Common
{
    public interface IMessengerAdapter
    {
        void RegisterMessageHandler<T>(object recipient, Action<T> messageHandler);
        void Send<T>(T value);
    }
}