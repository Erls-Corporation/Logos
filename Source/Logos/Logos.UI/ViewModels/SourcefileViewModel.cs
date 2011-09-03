using Logos.ReadModel;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
namespace Logos.UI.ViewModels
{
    public sealed class SourcefileViewModel
    {
        readonly SourcefileDto _sourcefile;
        readonly ObservableCollection<TagViewModel> _tags;

        public SourcefileViewModel(SourcefileDto sourcefile)
        {
            _sourcefile = sourcefile;

            _tags = new ObservableCollection<TagViewModel>();
        }

        public Guid RepositoryId
        {
            get
            {
                return _sourcefile.RepositoryId;
            }
        }

        public string Name
        {
            get
            {
                return _sourcefile.Name;
            }
        }

        public ObservableCollection<TagViewModel> Tags
        {
            get
            {
                return _tags;
            }
        }

        public void AddTags(IEnumerable<string> tags)
        {
            _tags.Clear();

            foreach (string currentTag in tags)
            {
                AddTag(currentTag);
            }
        }

        void AddTag(string tag)
        {
            _tags.Add(new TagViewModel(tag));
        }
    }
}