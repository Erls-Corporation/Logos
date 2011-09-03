using System.Windows.Input;
using System;
using Logos.Infrastructure;
using Logos.ApplicationServices.Commands;
using Logos.Domain.Core;
using Logos.Infrastructure.Persistence;
using Logos.ReadModel;
namespace Logos.UI.Commands
{
    public sealed class ImportRepositoryCommand
    {
        readonly ICommandSender _commandSender;
        readonly IGithubReadModel _readModel;
        readonly ICommand _value;

        public ImportRepositoryCommand(ICommandSender commandSender, IGithubReadModel readModel)
        {
            _commandSender = commandSender;
            _readModel = readModel;
            _value = new RelayCommand(obj => this.Import());
        }

        public ICommand Value
        {
            get
            {
                return _value;
            }
        }

        public string User { get; set; }
        public string ApiToken { get; set; }
        public string Repository { get; set; }

        void Import()
        {
            Guid importedRepositoryId = Guid.NewGuid();

            _commandSender.Send(new ImportRepository(importedRepositoryId, Repository, new Credentials(new Username(User), new ApiToken(ApiToken))));

            RepositoryListDto importedRepository = _readModel.GetRepositoryListById(importedRepositoryId);

            RepositoryImported(this, new RepositoryImportedEventArgs(importedRepository));
        }

        public event EventHandler<RepositoryImportedEventArgs> RepositoryImported = delegate { };
    }
}