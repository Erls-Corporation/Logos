using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logos.Domain.Events;
using System.Linq;

namespace Logos.Domain.Core
{
    public sealed class Repository : IAggregateRoot
    {
        EventApplier _eventApplier;
        string _name;
        readonly List<Sourcefile> _sourcefilenames;
        Guid _id;

        public Repository()
        {
            _eventApplier = new EventApplier(this);
            _sourcefilenames = new List<Sourcefile>();
        }

        public Repository(Guid id, string name, List<string> sourcefilenames)
        {
            _eventApplier = new EventApplier(this);
            _eventApplier.Apply(new RepositoryImported(id, name));
            _sourcefilenames = new List<Sourcefile>();

            ImportSourcefiles(sourcefilenames);
        }

        public Guid Id
        {
            get
            {
                return _id;
            }
        }

        public IEnumerable<IDomainEvent> GetUncommittedChanges()
        {
            return _eventApplier.GetAppliedChanges();
        }

        public void LoadFromHistory(IEnumerable<IDomainEvent> changes)
        {
            EventHistoryApplier historyApplier = new EventHistoryApplier(this);
            historyApplier.Apply(changes);
        }

        public void MarkChangesAsCommitted()
        {
            _eventApplier = new EventApplier(this);
        }

        public void Tag(string sourcefile, string newTag)
        {
            if (string.IsNullOrWhiteSpace(sourcefile))
            {
                throw new ArgumentNullException("sourcefile");
            }

            if (string.IsNullOrWhiteSpace(newTag))
            {
                throw new ArgumentNullException("newTag");
            }

            _eventApplier.Apply(new SourcefileTagged(_id, sourcefile, newTag));
        }

        void Apply(RepositoryImported importedEvent)
        {
            _id = importedEvent.GithubRepositoryId;
            _name = importedEvent.RepositoryName;
        }

        void Apply(SourcefileImported importedEvent)
        {
            _sourcefilenames.Add(new Sourcefile(importedEvent.Sourcefilename));
        }

        void Apply(SourcefileTagged tagEvent)
        {
            Sourcefile sourcefile = GetSourcefileByName(tagEvent.Sourcefile);

            sourcefile.Tag(tagEvent.NewTag);
        }

        void ImportSourcefiles(List<string> sourcefilenames)
        {
            foreach (string currentFilename in sourcefilenames)
            {
                _eventApplier.Apply(new SourcefileImported(_id, currentFilename));
            }
        }

        Sourcefile GetSourcefileByName(string name)
        {
            return _sourcefilenames.Where(sourcefile => sourcefile.Filename == name).FirstOrDefault();
        }
    }
}
