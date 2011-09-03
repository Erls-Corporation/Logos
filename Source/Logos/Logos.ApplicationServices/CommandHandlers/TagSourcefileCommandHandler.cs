using Logos.ApplicationServices.Commands;
using Logos.Domain;
using Logos.Domain.Core;
namespace Logos.ApplicationServices.CommandHandlers
{
    public class TagSourcefileCommandHandler : ICqrsCommandHandler<TagSourcefile>
    {
        readonly IGithubRepositoryRepository _githubRepository;

        public TagSourcefileCommandHandler(IGithubRepositoryRepository githubRepository)
        {
            _githubRepository = githubRepository;
        }

        public void Handle(TagSourcefile command)
        {
            Repository githubRepo = _githubRepository.GetAggregateById<Repository>(command.RepositoryId);

            githubRepo.Tag(command.Sourcefile, command.NewTag);

            _githubRepository.Save(githubRepo, command.OriginalVersion);
        }
    }
}