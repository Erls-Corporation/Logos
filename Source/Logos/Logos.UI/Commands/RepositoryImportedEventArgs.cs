using System;
using Logos.ReadModel;
namespace Logos.UI.Commands
{
    public sealed class RepositoryImportedEventArgs : EventArgs
    {
        readonly RepositoryListDto _importedRepository;

        public RepositoryImportedEventArgs(RepositoryListDto importedRepository)
        {
            _importedRepository = importedRepository;
        }

        public RepositoryListDto ImportedRepository
        {
            get
            {
                return _importedRepository;
            }
        }
    }
}