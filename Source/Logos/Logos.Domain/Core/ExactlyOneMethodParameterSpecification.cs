using System.Reflection;
namespace Logos.Domain.Core
{
    public sealed class ExactlyOneMethodParameterSpecification : ISpecification<MethodInfo>
    {
        public bool IsSatisfiedBy(MethodInfo value)
        {
            bool answer = value.GetParameters().Length == 1;
            return answer;
        }
    }
}