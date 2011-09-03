using System.Reflection;
namespace Logos.Domain.Core
{
    public class MethodQueryCriteria
    {
        readonly object _parameter;

        public MethodQueryCriteria(object parameter)
        {
            _parameter = parameter;
        }

        public bool Match(MethodInfo method)
        {
            TypeInformationExtractor typeExtractor = new TypeInformationExtractor(_parameter);

            ISpecification<MethodInfo> exactlyOneParameter = new ExactlyOneMethodParameterSpecification();
            ISpecification<MethodInfo> parameterTypeMatch = new FirstMethodParameterTypeSpecification(typeExtractor.ExtractType());

            ISpecification<MethodInfo> exactlyOneParameterAndParameterTypeMatch = new AndSpecification<MethodInfo>(exactlyOneParameter, parameterTypeMatch);

            return exactlyOneParameterAndParameterTypeMatch.IsSatisfiedBy(method);
        }
    }
}