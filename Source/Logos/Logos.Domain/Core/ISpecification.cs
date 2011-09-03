namespace Logos.Domain.Core
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T value);
    }
}