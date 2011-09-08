using System;
using Logos.Domain.Events;
using System.Collections.Generic;

namespace Logos.Infrastructure.Persistence
{
    public interface IEventStore
    {
        void SaveEvents(Guid aggregateId, IEnumerable<DomainEvent> newEvents, int expectedVersion);
        IEnumerable<DomainEvent> GetEventsForAggregate(Guid aggregateId);
        void ReplayAllEvents();
    }
}