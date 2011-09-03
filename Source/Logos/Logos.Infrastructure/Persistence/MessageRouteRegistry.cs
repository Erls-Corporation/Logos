using System.Collections.Generic;
using System;
using Logos.Domain;

namespace Logos.Infrastructure.Persistence
{
    public sealed class MessageRouteRegistry
    {
        readonly Dictionary<Type, List<Action<IMessage>>> _messageroutes;

        public MessageRouteRegistry()
        {
            _messageroutes = new Dictionary<Type, List<Action<IMessage>>>();
        }

        public void AddMessageHandler<T>(Action<IMessage> handler)
        {
            List<Action<IMessage>> messageHandlers;

            if (!_messageroutes.TryGetValue(typeof(T), out messageHandlers))
            {
                messageHandlers = new List<Action<IMessage>>();
                _messageroutes.Add(typeof(T), messageHandlers);
            }

            messageHandlers.Add(handler);
        }

        public Action<IMessage> GetMessageHandler<T>() where T : IMessage
        {
            List<Action<IMessage>> messageHandlers = GetMessageHandlers<T>();

            if (messageHandlers == null)
            {
                throw new InvalidOperationException("No route found for given message.");
            }

            return GetMessageHandlers<T>()[0];
        }

        public List<Action<IMessage>> GetMessageHandlers<T>()
        {
            List<Action<IMessage>> messageHandlers;

            if (_messageroutes.TryGetValue(typeof(T), out messageHandlers))
            {
                return messageHandlers;
            }

            return null;
        }

        public List<Action<IMessage>> GetMessageHandlers(Type concreteType)
        {
            List<Action<IMessage>> messageHandlers;

            if (_messageroutes.TryGetValue(concreteType, out messageHandlers))
            {
                return messageHandlers;
            }

            return null;
        }
    }
}