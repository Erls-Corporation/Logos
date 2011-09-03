namespace Logos.Domain.Events
{
    public interface IDomainEvent : IMessage
    {
        int VersionNumber { get; }
        DomainEventVersion Version { get; }
        void AssignVersion(DomainEventVersion newVersion);
    }
}