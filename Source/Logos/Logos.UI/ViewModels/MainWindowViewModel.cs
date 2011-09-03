using System;
using System.Collections.Generic;
using System.Linq;
using Logos.ApplicationServices.Commands;
using Logos.Infrastructure;
using Logos.ReadModel;
using System.ComponentModel;

namespace Logos.UI.ViewModels
{
    public sealed class MainWindowViewModel : INotifyPropertyChanged
    {
        readonly Func<RepositoryListViewModel> _repositoryListFactory;
        RepositoryListViewModel _repositoryList;

        public MainWindowViewModel(Func<RepositoryListViewModel> repositoryListFactory)
        {
            _repositoryListFactory = repositoryListFactory;

            RepositoryList = _repositoryListFactory();
        }

        public RepositoryListViewModel RepositoryList
        {
            get
            {
                return _repositoryList;
            }

            set
            {
                _repositoryList = value;

                PropertyChanged(this, new PropertyChangedEventArgs("RepositoryList"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}