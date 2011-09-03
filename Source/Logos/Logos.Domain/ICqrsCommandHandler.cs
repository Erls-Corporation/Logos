namespace Logos.Domain
{
    public interface ICqrsCommandHandler<T>
        where T : ICqrsCommand
    {
        void Handle(T command);
    }
}