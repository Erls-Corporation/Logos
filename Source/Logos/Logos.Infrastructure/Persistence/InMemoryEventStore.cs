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
        readonly EventVersionizer _versionizer;

        public InMemoryEventStore(IEventPublisher publisher)
        {
            _publisher = publisher;
            _eventStorage = new Dictionary<Guid, List<EventDescriptor>>();
            _versionizer = new EventVersionizer();
        }

        public void SaveEvents(Guid aggregateId, IEnumerable<DomainEvent> newEvents, int expectedVersion)
        {
            _versionizer.Versionize(newEvents, expectedVersion);
            SaveNewEvents(aggregateId, newEvents);
            PublishNewEvents(newEvents);
        }

        public IEnumerable<DomainEvent> GetEventsForAggregate(Guid aggregateId)
        {
            return GetSavedEvents(aggregateId).Select(eventDescriptor => eventDescriptor.EventData).ToList();
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

        void SaveNewEvents(Guid aggregateId, IEnumerable<DomainEvent> newEvents)
        {
            foreach (DomainEvent newEvent in newEvents)
            {
                Save(aggregateId, newEvent);
            }
        }

        void Save(Guid aggregateId, DomainEvent newEvent)
        {
            List<EventDescriptor> savedEvents = GetSavedEvents(aggregateId);

            savedEvents.Add(new EventDescriptor(aggregateId, newEvent));
        }

        void PublishNewEvents(IEnumerable<DomainEvent> newEvents)
        {
            foreach (DomainEvent newEvent in newEvents)
            {
                Publish(newEvent);
            }
        }

        void Publish(DomainEvent unpublishedEvent)
        {
            _publisher.Publish(unpublishedEvent);
        }

        sealed class EventDescriptor
        {
            readonly Guid _id;
            readonly DomainEvent _eventData;

            public EventDescriptor(Guid id, DomainEvent eventData)
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

            public DomainEvent EventData
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
                    return _eventData.Version;
                }
            }
        }


        public void ReplayAllEvents()
        {
        }
    }
}