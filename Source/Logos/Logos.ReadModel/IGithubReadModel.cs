using System.Collections.Generic;
using System;
namespace Logos.ReadModel
{
    public interface IGithubReadModel
    {
        RepositoryListDto GetRepositoryListById(Guid repositoryId);
        IEnumerable<string> GetTagsBySourcefile(Guid repositoryId, string sourcefile);
        IEnumerable<RepositoryListDto> GetAllRepositories();
    }
}