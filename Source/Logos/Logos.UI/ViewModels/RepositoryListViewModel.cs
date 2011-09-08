using Logos.ReadModel;
using System;
using Logos.UI.Commands;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Logos.Infrastructure.Common;
using Logos.Infrastructure.Persistence;
using System.Linq;
using Logos.ApplicationServices.Commands;
using System.Collections.Generic;

namespace Logos.UI.ViewModels
{
    public class RepositoryListViewModel
    {
        readonly ImportRepositoryCommand _importRepositoryCommand;
        readonly Func<RepositoryListDto, RepositoryViewModel> _repositoryFactory;
        readonly ICommandSender _commandSender;
        readonly IGithubReadModel _readModel;
        readonly ObservableCollection<RepositoryViewModel> _repositories;

        public RepositoryListViewModel(
            ImportRepositoryCommand importRepositoryCommand,
            Func<RepositoryListDto, RepositoryViewModel> repositoryFactory,
            ICommandSender commandSender,
            IGithubReadModel readModel)
        {
            _importRepositoryCommand = importRepositoryCommand;
            _repositoryFactory = repositoryFactory;
            _commandSender = commandSender;
            _readModel = readModel;
            _repositories = new ObservableCollection<RepositoryViewModel>();

            _importRepositoryCommand.RepositoryImported += RepositoryImportedEventHandler;

            Initialize();
        }

        public string User
        {
            get
            {
                return _importRepositoryCommand.User;
            }

            set
            {
                _importRepositoryCommand.User = value;
            }
        }

        public string ApiToken
        {
            get
            {
                return _importRepositoryCommand.ApiToken;
            }

            set
            {
                _importRepositoryCommand.ApiToken = value;
            }
        }

        public string Repository
        {
            get
            {
                return _importRepositoryCommand.Repository;
            }

            set
            {
                _importRepositoryCommand.Repository = value;
            }
        }

        public string NewTag { get; set; }

        public ICommand ImportRepositoryCommand
        {
            get
            {
                return _importRepositoryCommand.Value;
            }
        }

        public ObservableCollection<RepositoryViewModel> Repositories
        {
            get
            {
                return _repositories;
            }
        }

        void Initialize()
        {
            var repositories = _readModel.GetAllRepositories().ToList();

            foreach (RepositoryListDto currentRepository in repositories)
            {
                RepositoryViewModel newRepositoryViewModel = _repositoryFactory(currentRepository);

                _repositories.Add(newRepositoryViewModel);
            }
        }

        void RepositoryImportedEventHandler(object sender, RepositoryImportedEventArgs e)
        {
            _repositories.Add(_repositoryFactory(e.ImportedRepository));
        }

        public void TagSourcefile(SourcefileViewModel sourcefile)
        {
            RepositoryViewModel repository = GetRepositoryById(sourcefile.RepositoryId);

            _commandSender.Send(new TagSourcefile(repository.Id, sourcefile.Name, NewTag, repository.Version));

            var tags = _readModel.GetTagsBySourcefile(repository.Id, sourcefile.Name);

            sourcefile.AddTags(tags);
        }

        RepositoryViewModel GetRepositoryById(Guid id)
        {
            return _repositories.Where(repo => repo.Id == id).FirstOrDefault();
        }
    }
}