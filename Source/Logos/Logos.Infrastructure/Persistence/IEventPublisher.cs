using Logos.Domain.Events;

namespace Logos.Infrastructure.Persistence
{
    public interface IEventPublisher
    {
        void Publish<T>(T domainEvent) where T : DomainEvent;
    }
}