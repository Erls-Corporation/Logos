using System;
namespace Logos.Domain.Events
{
    public sealed class SourcefileTagged : IDomainEvent
    {
        readonly Guid _repositoryId;
        readonly string _sourcefile;
        readonly string _newTag;
        DomainEventVersion _version;

        public SourcefileTagged(Guid repositoryId, string sourcefile, string newTag)
        {
            _repositoryId = repositoryId;
            _sourcefile = sourcefile;
            _newTag = newTag;
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

        public string NewTag
        {
            get
            {
                return _newTag;
            }
        }

        public Guid RepositoryId
        {
            get
            {
                return _repositoryId;
            }
        }

        public string Sourcefile
        {
            get
            {
                return _sourcefile;
            }
        }

        public void AssignVersion(DomainEventVersion newVersion)
        {
            _version = newVersion;
        }
    }
}