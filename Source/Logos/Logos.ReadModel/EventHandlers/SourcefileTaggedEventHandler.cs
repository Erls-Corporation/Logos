using Logos.Domain.Events;
namespace Logos.ReadModel.EventHandlers
{
    public sealed class SourcefileTaggedEventHandler : IDomainEventHandler<SourcefileTagged>
    {
        readonly IQueryDatabase _queryDatabase;

        public SourcefileTaggedEventHandler(IQueryDatabase queryDatabase)
        {
            _queryDatabase = queryDatabase;
        }

        public void Handle(SourcefileTagged domainEvent)
        {
            _queryDatabase.AddRepositoryListSourcefileTag(domainEvent.RepositoryId, domainEvent.VersionNumber, domainEvent.Sourcefile, domainEvent.NewTag);
        }
    }
}