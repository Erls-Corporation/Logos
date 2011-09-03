using System;
using System.Collections.Generic;
namespace Logos.ReadModel
{
    public sealed class SourcefileDto
    {
        readonly Guid _repositoryId;
        readonly string _name;
        readonly List<string> _tags;

        public SourcefileDto(Guid repositoryId, string name)
        {
            _repositoryId = repositoryId;
            _name = name;
            _tags = new List<string>();
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public Guid RepositoryId
        {
            get
            {
                return _repositoryId;
            }
        }

        public IEnumerable<string> Tags
        {
            get
            {
                return new List<string>(_tags);
            }
        }

        public void AddTag(string newTag)
        {
            _tags.Add(newTag);
        }
    }
}