using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Logos.Domain.Events
{
    [DataContract]
    public sealed class RepositoryImported : DomainEvent
    {
        public RepositoryImported()
        {
        }

        public RepositoryImported(Guid githubRepositoryId, string repositoryName)
        {
            GithubRepositoryId = githubRepositoryId;
            RepositoryName = repositoryName;
        }

        [DataMember]
        public Guid GithubRepositoryId { get; set; }

        [DataMember]
        public string RepositoryName { get; set; }
    }
}