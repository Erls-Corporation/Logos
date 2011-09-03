using System;
using Logos.Domain;

namespace Logos.Infrastructure.Persistence
{
    public class InMemoryCommandMessageBus : ICommandSender
    {
        readonly MessageRouteRegistry _routes;

        public InMemoryCommandMessageBus()
        {
            _routes = new MessageRouteRegistry();
        }

        public void RegisterHandler<T>(Action<T> handler) where T : IMessage
        {
            _routes.AddMessageHandler<T>(DelegateAdjuster.CastArgument<IMessage, T>(x => handler(x)));
        }

        public void Send<T>(T command) where T : ICqrsCommand
        {
            Action<IMessage> commandHandler = _routes.GetMessageHandler<T>();

            commandHandler(command);
        }
    }
}