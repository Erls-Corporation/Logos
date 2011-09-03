using System.Collections.Generic;
using System;
namespace Logos.Domain.Core
{
    public interface IGithubRepositoryRepository
    {
        Repository Import(Guid repositoryId, string repositoryName, Credentials credentials);
        void Save(IAggregateRoot aggregateRoot, int expectedVersion);

        T GetAggregateById<T>(Guid aggregateId) where T : IAggregateRoot, new();
    }
}