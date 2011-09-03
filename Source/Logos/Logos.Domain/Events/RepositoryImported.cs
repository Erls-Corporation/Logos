using System;
using System.Collections.Generic;
namespace Logos.Domain.Events
{
    public sealed class RepositoryImported : IDomainEvent
    {
        readonly Guid _githubRepositoryId;
        readonly string _repositoryName;
        DomainEventVersion _version;

        public RepositoryImported(Guid githubRepositoryId, string repositoryName)
        {
            _githubRepositoryId = githubRepositoryId;
            _repositoryName = repositoryName;
            _version = new DomainEventVersion();
        }

        public int VersionNumber
        {
            get
            {
                return _version.Value;
            }
        }

        public DomainEventVersion Version
        {
            get
            {
                return _version;
            }
        }

        public Guid GithubRepositoryId
        {
            get
            {
                return _githubRepositoryId;
            }
        }

        public string RepositoryName
        {
            get
            {
                return _repositoryName;
            }
        }

        public void AssignVersion(DomainEventVersion newVersion)
        {
            _version = newVersion;
        }
    }
}