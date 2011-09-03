using System.Reflection;
using System;
namespace Logos.Domain.Core
{
    public class FirstMethodParameterTypeSpecification : ISpecification<MethodInfo>
    {
        readonly Type _parameterType;

        public FirstMethodParameterTypeSpecification(Type parameterType)
        {
            _parameterType = parameterType;
        }

        public bool IsSatisfiedBy(MethodInfo value)
        {
            Type methodType = value.GetParameters()[0].ParameterType;
            bool answer = value.GetParameters()[0].ParameterType == _parameterType;
            return answer;
        }
    }
}