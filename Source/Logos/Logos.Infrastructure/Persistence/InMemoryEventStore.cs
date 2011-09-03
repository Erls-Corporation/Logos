using System;
using System.Collections.Generic;
using Logos.Domain.Events;
using System.Linq;

namespace Logos.Infrastructure.Persistence
{
    public sealed class InMemoryEventStore : IEventStore
    {
        readonly IEventPublisher _publisher;
        readonly Dictionary<Guid, List<EventDescriptor>> _eventStorage;

        public InMemoryEventStore(IEventPublisher publisher)
        {
            _publisher = publisher;
            _eventStorage = new Dictionary<Guid, List<EventDescriptor>>();
        }

        public void SaveEvents(Guid aggregateId, IEnumerable<IDomainEvent> newEvents, int expectedVersion)
        {
            AssignVersionNumbers(newEvents, expectedVersion);
            SaveNewEvents(aggregateId, newEvents);
            PublishNewEvents(newEvents);
        }

        public IEnumerable<IDomainEvent> GetEventsForAggregate(Guid aggregateId)
        {
            return GetSavedEvents(aggregateId).Select(eventDescriptor => eventDescriptor.EventData).ToList();
        }

        void AssignVersionNumbers(IEnumerable<IDomainEvent> newEvents, int expectedVersion)
        {
            DomainEventVersion currentVersion = new DomainEventVersion(expectedVersion);
            foreach (IDomainEvent newEvent in newEvents)
            {
                currentVersion = currentVersion.Increment();
                newEvent.AssignVersion(currentVersion);
            }
        }

        List<EventDescriptor> GetSavedEvents(Guid aggregateId)
        {
            List<EventDescriptor> savedEvents;
            if (_eventStorage.TryGetValue(aggregateId, out savedEvents))
            {
                return savedEvents;
            }
            else
            {
                savedEvents = new List<EventDescriptor>();
                _eventStorage.Add(aggregateId, savedEvents);

                return savedEvents;
            }
        }

        void SaveNewEvents(Guid aggregateId, IEnumerable<IDomainEvent> newEvents)
        {
            foreach (IDomainEvent newEvent in newEvents)
            {
                Save(aggregateId, newEvent);
            }
        }

        void Save(Guid aggregateId, IDomainEvent newEvent)
        {
            List<EventDescriptor> savedEvents = GetSavedEvents(aggregateId);

            savedEvents.Add(new EventDescriptor(aggregateId, newEvent));
        }

        void PublishNewEvents(IEnumerable<IDomainEvent> newEvents)
        {
            foreach (IDomainEvent newEvent in newEvents)
            {
                Publish(newEvent);
            }
        }

        void Publish(IDomainEvent unpublishedEvent)
        {
            _publisher.Publish(unpublishedEvent);
        }

        sealed class EventDescriptor
        {
            readonly Guid _id;
            readonly IDomainEvent _eventData;

            public EventDescriptor(Guid id, IDomainEvent eventData)
            {
                _id = id;
                _eventData = eventData;
            }

            public Guid Id
            {
                get
                {
                    return _id;
                }
            }

            public IDomainEvent EventData
            {
                get
                {
                    return _eventData;
                }
            }

            public int Version
            {
                get
                {
                    return _eventData.VersionNumber;
                }
            }
        }
    }
}