using System.Collections.Generic;
using System;
namespace Logos.ReadModel
{
    public sealed class GithubReadModel : IGithubReadModel
    {
        readonly IQueryDatabase _queryDatabase;

        public GithubReadModel(IQueryDatabase queryDatabase)
        {
            _queryDatabase = queryDatabase;
        }

        public RepositoryListDto GetRepositoryListById(Guid repositoryId)
        {
            return _queryDatabase.GetRepositoryListById(repositoryId);
        }

        public IEnumerable<string> GetTagsBySourcefile(Guid repositoryId, string sourcefile)
        {
            return _queryDatabase.GetTagsBySourcefile(repositoryId, sourcefile);
        }
    }
}