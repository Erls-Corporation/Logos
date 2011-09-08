using System.Collections.Generic;
using System;
using System.Linq;

namespace Logos.ReadModel
{
    public sealed class QueryDatabase : IQueryDatabase
    {
        readonly List<RepositoryListDto> _repositoryLists;

        public QueryDatabase()
        {
            _repositoryLists = new List<RepositoryListDto>();
        }

        public void AddRepositoryList(RepositoryListDto repositoryList)
        {
            _repositoryLists.Add(repositoryList);
        }

        public void AddRepositoryListSourcefile(Guid repositoryListId, int version, SourcefileDto sourcefile)
        {
            RepositoryListDto repositoryList = GetRepositoryListById(repositoryListId);

            repositoryList.AddSourcefile(version, sourcefile);
        }

        public RepositoryListDto GetRepositoryListById(Guid repositoryListId)
        {
            return _repositoryLists.Where(list => list.Id == repositoryListId).FirstOrDefault();
        }


        public void AddRepositoryListSourcefileTag(Guid repositoryListId, int version, string sourcefilename, string tag)
        {
            RepositoryListDto repository = GetRepositoryListById(repositoryListId);

            repository.AddTag(version, sourcefilename, tag);
        }

        SourcefileDto GetSourcefileByName(Guid repositoryId, string name)
        {
            RepositoryListDto repository = GetRepositoryListById(repositoryId);

            return repository.Sourcefiles.Where(sourcefile => sourcefile.Name == name).FirstOrDefault();
        }


        public IEnumerable<string> GetTagsBySourcefile(Guid repositoryId, string sourcefileName)
        {
            SourcefileDto sourcefile = GetSourcefileByName(repositoryId, sourcefileName);

            return sourcefile.Tags;
        }


        public IEnumerable<RepositoryListDto> GetAllRepositories()
        {
            return _repositoryLists.AsEnumerable();
        }
    }
}