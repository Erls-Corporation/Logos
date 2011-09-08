using System;
using System.Runtime.Serialization;
namespace Logos.Domain.Events
{
    [DataContract]
    public sealed class SourcefileTagged : DomainEvent
    {
        public SourcefileTagged()
        {
        }

        public SourcefileTagged(Guid repositoryId, string sourcefile, string newTag)
        {
            RepositoryId = repositoryId;
            Sourcefile = sourcefile;
            NewTag = newTag;
        }

        [DataMember]
        public string NewTag { get; set; }

        [DataMember]
        public Guid RepositoryId { get; set; }

        [DataMember]
        public string Sourcefile { get; set; }
    }
}