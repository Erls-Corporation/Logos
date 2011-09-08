using Logos.Domain.Events;
namespace Logos.ReadModel.EventHandlers
{
    public sealed class SourcefileImportedEventHandler : IDomainEventHandler<SourcefileImported>
    {
        readonly IQueryDatabase _queryDatabase;

        public SourcefileImportedEventHandler(IQueryDatabase queryDatabase)
        {
            _queryDatabase = queryDatabase;
        }

        public void Handle(SourcefileImported domainEvent)
        {
            _queryDatabase.AddRepositoryListSourcefile(domainEvent.GithubRepositoryId, domainEvent.Version, new SourcefileDto(domainEvent.GithubRepositoryId, domainEvent.Sourcefilename));
        }
    }
}