using Logos.Domain.Events;

namespace Logos.ReadModel.EventHandlers
{
    public sealed class RepositoryImportedEventHandler : IDomainEventHandler<RepositoryImported>
    {
        readonly IQueryDatabase _queryDatabase;

        public RepositoryImportedEventHandler(IQueryDatabase queryDatabase)
        {
            _queryDatabase = queryDatabase;
        }

        public void Handle(RepositoryImported domainEvent)
        {
            _queryDatabase.AddRepositoryList(new RepositoryListDto(domainEvent.GithubRepositoryId, domainEvent.RepositoryName, domainEvent.Version));
        }
    }
}