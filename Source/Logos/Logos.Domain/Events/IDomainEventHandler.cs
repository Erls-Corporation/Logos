namespace Logos.Domain.Events
{
    public interface IDomainEventHandler<T>
        where T : DomainEvent
    {
        void Handle(T domainEvent);
    }
}