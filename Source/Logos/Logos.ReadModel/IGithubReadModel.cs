using System.Collections.Generic;
using System;
namespace Logos.ReadModel
{
    public interface IGithubReadModel
    {
        RepositoryListDto GetRepositoryListById(Guid repositoryId);
        RepositoryListDto GetRepositoryListByName(string repositoryName);
        IEnumerable<string> GetTagsBySourcefile(Guid repositoryId, string sourcefile);
        IEnumerable<RepositoryListDto> GetAllRepositories();
    }
}