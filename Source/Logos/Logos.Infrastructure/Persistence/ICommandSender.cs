using Logos.Domain;

namespace Logos.Infrastructure.Persistence
{
    public interface ICommandSender
    {
        void Send<T>(T command) where T : ICqrsCommand;
    }
}