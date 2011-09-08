using Logos.Domain.Events;
using System.Collections.Generic;
namespace Logos.Infrastructure.Persistence
{
    internal sealed class EventVersionizer
    {
        public EventVersionizer()
        {
        }

        public void Versionize(IEnumerable<IDomainEvent> events, int expectedVersion)
        {
            DomainEventVersion currentVersion = new DomainEventVersion(expectedVersion);
            foreach (IDomainEvent newEvent in events)
            {
                currentVersion = currentVersion.Increment();
                newEvent.AssignVersion(currentVersion);
            }
        }
    }
}