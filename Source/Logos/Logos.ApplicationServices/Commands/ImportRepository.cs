using Logos.Domain;
using System;
using System.Collections.Generic;
using Logos.Domain.Core;
namespace Logos.ApplicationServices.Commands
{
    public sealed class ImportRepository : ICqrsCommand
    {
        readonly Guid _githubRepositoryId;
        readonly string _repositoryName;
        readonly Credentials _credentials;

        public ImportRepository(Guid githubRepositoryId, string repositoryName, Credentials credentials)
        {
            _githubRepositoryId = githubRepositoryId;
            _repositoryName = repositoryName;
            _credentials = credentials;
        }

        public Guid GithubRepositoryId
        {
            get
            {
                return _githubRepositoryId;
            }
        }

        public string RepositoryName
        {
            get
            {
                return _repositoryName;
            }
        }

        public Credentials Credentials
        {
            get
            {
                return _credentials;
            }
        }
    }
}