using System;
using System.Runtime.Serialization;
namespace Logos.Domain.Events
{
    [DataContract]
    public sealed class SourcefileImported : DomainEvent
    {
        public SourcefileImported()
        {
        }

        public SourcefileImported(Guid githubRepositoryId, string sourcefilename)
        {
            GithubRepositoryId = githubRepositoryId;
            Sourcefilename = sourcefilename;
        }

        [DataMember]
        public string Sourcefilename { get; set; }

        [DataMember]
        public Guid GithubRepositoryId { get; set; }
    }
}