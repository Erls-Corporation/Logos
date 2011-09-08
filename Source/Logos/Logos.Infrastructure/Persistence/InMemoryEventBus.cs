using Logos.Domain;
using System.Collections.Generic;
using System;
using Logos.Domain.Events;

namespace Logos.Infrastructure.Persistence
{
    public class InMemoryEventBus : IEventPublisher
    {
        readonly MessageRouteRegistry _routes;

        public InMemoryEventBus()
        {
            _routes = new MessageRouteRegistry();
        }

        public void RegisterHandler<T>(Action<T> handler) where T : IMessage
        {
            _routes.AddMessageHandler<T>(DelegateAdjuster.CastArgument<IMessage, T>(x => handler(x)));
        }

        public void Publish<T>(T domainEvent) where T : DomainEvent
        {
            List<Action<IMessage>> eventHandlers = _routes.GetMessageHandlers(domainEvent.GetType());

            if (eventHandlers == null)
            {
                return;
            }

            foreach (Action<IMessage> currentEventHandler in eventHandlers)
            {
                currentEventHandler(domainEvent);
            }
        }
    }
}