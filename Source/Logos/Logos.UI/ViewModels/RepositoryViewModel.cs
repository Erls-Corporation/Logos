using System;
using System.Collections.ObjectModel;
using Logos.ReadModel;
using System.Collections.Generic;
namespace Logos.UI.ViewModels
{
    public sealed class RepositoryViewModel
    {
        readonly RepositoryListDto _repository;
        readonly Func<SourcefileDto, SourcefileViewModel> _sourcefileFactory;

        readonly ObservableCollection<SourcefileViewModel> _sourcefiles;

        public RepositoryViewModel(RepositoryListDto repository, Func<SourcefileDto, SourcefileViewModel> sourcefileFactory)
        {
            _repository = repository;
            _sourcefileFactory = sourcefileFactory;

            _sourcefiles = new ObservableCollection<SourcefileViewModel>();

            InitializeSourcefiles();
        }

        public string Name
        {
            get
            {
                return _repository.Name;
            }
        }

        public Guid Id
        {
            get
            {
                return _repository.Id;
            }
        }

        public ObservableCollection<SourcefileViewModel> Sourcefiles
        {
            get
            {
                return _sourcefiles;
            }
        }

        public int Version
        {
            get
            {
                return _repository.Version;
            }
        }

        void AddSourcefile(SourcefileDto sourcefile)
        {
            _sourcefiles.Add(_sourcefileFactory(sourcefile));
        }

        void InitializeSourcefiles()
        {
            IEnumerable<SourcefileDto> sourcefiles = _repository.Sourcefiles;
            foreach (SourcefileDto currentSourcefile in sourcefiles)
            {
                AddSourcefile(currentSourcefile);
            }
        }
    }
}