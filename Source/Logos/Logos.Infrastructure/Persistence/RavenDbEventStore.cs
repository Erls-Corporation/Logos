using System;
using System.Collections.Generic;
using Logos.Domain.Events;
using System.Linq;
using Raven.Client.Document;
using Raven.Client;

namespace Logos.Infrastructure.Persistence
{
    public sealed class RavenDbEventStore : IEventStore
    {
        readonly IEventPublisher _publisher;
        readonly IDocumentStore _eventStorage;
        readonly EventVersionizer _versionizer;

        public RavenDbEventStore(IEventPublisher publisher, IDocumentStore eventStorage)
        {
            _publisher = publisher;
            _eventStorage = eventStorage;
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
            using (IDocumentSession session = _eventStorage.OpenSession())
            {
                var events = from eventDescriptor in session.Query<EventDescriptor>()
                       where eventDescriptor.AggregateId == aggregateId
                       orderby eventDescriptor.Version
                       select eventDescriptor;

                JsonDeserializer deserializer = new JsonDeserializer();

                List<DomainEvent> retrievedEvents = new List<DomainEvent>();
                foreach (EventDescriptor ev in events)
                {
                    retrievedEvents.Add(deserializer.Deserialize(ev.EventType, ev.EventData));
                }


                return retrievedEvents.AsEnumerable();
            }
        }

        public void ReplayAllEvents()
        {
            using (IDocumentSession session = _eventStorage.OpenSession())
            {
                var events = (from eventDescriptor in session.Query<EventDescriptor>()
                              orderby eventDescriptor.AggregateId, eventDescriptor.Version
                             select eventDescriptor).ToList();

                JsonDeserializer deserializer = new JsonDeserializer();

                foreach (EventDescriptor ev in events)
                {
                    Publish(deserializer.Deserialize(ev.EventType, ev.EventData));
                }
            }
        }

        void SaveNewEvents(Guid aggregateId, IEnumerable<DomainEvent> newEvents)
        {
            using (IDocumentSession session = _eventStorage.OpenSession())
            {
                foreach (DomainEvent newEvent in newEvents)
                {
                    var storableEvent = ConvertToStorableFormat(aggregateId, newEvent);
                    session.Store(storableEvent);
                }

                session.SaveChanges();
            }
        }

        EventDescriptor ConvertToStorableFormat(Guid aggregateId, DomainEvent newEvent)
        {
            var serializer = new JsonSerializer();
            string serializedEvent = serializer.Serialize(newEvent);

            return new EventDescriptor()
                {
                    AggregateId = aggregateId,
                    EventData = serializedEvent,
                    Version = newEvent.Version,
                    EventType = newEvent.GetType()
                };
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
            public string Id { get; set; }
            public Guid AggregateId { get; set; }
            public string EventData { get; set; }
            public int Version { get; set; }
            public Type EventType { get; set; }
        }
    }
}