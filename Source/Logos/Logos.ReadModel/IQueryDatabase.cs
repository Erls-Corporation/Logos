using System;
using System.Collections.Generic;
namespace Logos.ReadModel
{
    public interface IQueryDatabase
    {
        void AddRepositoryList(RepositoryListDto repositoryList);
        void AddRepositoryListSourcefile(Guid repositoryListId, int Version, SourcefileDto sourcefile);
        void AddRepositoryListSourcefileTag(Guid repositoryListId, int version, string sourcefilename, string tag);

        RepositoryListDto GetRepositoryListById(Guid repositoryId);
        IEnumerable<string> GetTagsBySourcefile(Guid repositoryId, string sourcefileName);
    }
}