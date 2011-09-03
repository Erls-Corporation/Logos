namespace Logos.Domain.Core
{
    public sealed class AndSpecification<T> : ISpecification<T>
    {
        readonly ISpecification<T> _leftSpecification;
        readonly ISpecification<T> _rightSpecification;

        public AndSpecification(ISpecification<T> leftSpecification, ISpecification<T> rightSpecification)
        {
            _leftSpecification = leftSpecification;
            _rightSpecification = rightSpecification;
        }

        public bool IsSatisfiedBy(T value)
        {
            return _leftSpecification.IsSatisfiedBy(value) && _rightSpecification.IsSatisfiedBy(value);
        }
    }
}