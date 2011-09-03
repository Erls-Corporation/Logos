using Logos.Domain;
using System;
namespace Logos.ApplicationServices.Commands
{
    public sealed class TagSourcefile : ICqrsCommand
    {
        readonly Guid _repositoryId;
        readonly string _sourcefile;
        readonly string _newTag;
        readonly int _originalVersion;

        public TagSourcefile(Guid repositoryId, string sourcefile, string newTag, int originalVersion)
        {
            _repositoryId = repositoryId;
            _sourcefile = sourcefile;
            _newTag = newTag;
            _originalVersion = originalVersion;
        }

        public int OriginalVersion
        {
            get
            {
                return _originalVersion;
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

        public string NewTag
        {
            get
            {
                return _newTag;
            }
        }
    }
}