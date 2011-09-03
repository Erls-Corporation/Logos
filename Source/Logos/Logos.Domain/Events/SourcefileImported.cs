using System;
namespace Logos.Domain.Events
{
    public sealed class SourcefileImported : IDomainEvent
    {
        readonly Guid _githubRepositoryId;
        readonly string _sourcefilename;
        DomainEventVersion _version;

        public SourcefileImported(Guid githubRepositoryId, string sourcefilename)
        {
            _githubRepositoryId = githubRepositoryId;
            _sourcefilename = sourcefilename;
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

        public string Sourcefilename
        {
            get
            {
                return _sourcefilename;
            }
        }

        public Guid GithubRepositoryId
        {
            get
            {
                return _githubRepositoryId;
            }
        }


        public void AssignVersion(DomainEventVersion newVersion)
        {
            _version = newVersion;
        }
    }
}