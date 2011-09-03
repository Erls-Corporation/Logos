using Logos.Domain.Core;
using GithubSharp.Core;
using System;

namespace Logos.Infrastructure.Persistence
{
    public sealed class GithubRepositoryRepository : IGithubRepositoryRepository
    {
        readonly IEventStore _storage;

        public GithubRepositoryRepository(IEventStore storage)
        {
            _storage = storage;
        }

        public Repository Import(Guid repositoryId, string repositoryName, Credentials credentials)
        {
            RepositoryFileApi githubApi = new RepositoryFileApi(credentials.Username, credentials.ApiToken, repositoryName);

            return new Repository(repositoryId, repositoryName, githubApi.GetFilenames());
        }

        public void Save(IAggregateRoot aggregateRoot, int expectedVersion)
        {
            _storage.SaveEvents(aggregateRoot.Id, aggregateRoot.GetUncommittedChanges(), expectedVersion);
            aggregateRoot.MarkChangesAsCommitted();
        }


        public T GetAggregateById<T>(Guid aggregateId) where T : IAggregateRoot, new()
        {
            var aggregate = new T();

            var e = _storage.GetEventsForAggregate(aggregateId);

            aggregate.LoadFromHistory(e);

            return aggregate;
        }
    }
}