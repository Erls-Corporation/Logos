using System;
using Logos.Domain.Events;
using System.Collections.Generic;

namespace Logos.Infrastructure.Persistence
{
    public interface IEventStore
    {
        void SaveEvents(Guid aggregateId, IEnumerable<IDomainEvent> newEvents, int expectedVersion);
        IEnumerable<IDomainEvent> GetEventsForAggregate(Guid aggregateId);
    }
}