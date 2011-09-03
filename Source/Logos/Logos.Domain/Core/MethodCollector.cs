using System;
using System.Reflection;
using System.Linq;

namespace Logos.Domain.Core
{
    public class MethodCollector
    {
        readonly object _target;

        public MethodCollector(object target)
        {
            _target = target;
        }

        public MethodInfo CollectFirstMatch(MethodQueryCriteria queryCriteria)
        {
            MethodInfo[] targetTypeMethods = GetTargetMethods();

            return targetTypeMethods.Where(method => queryCriteria.Match(method)).FirstOrDefault();
        }

        MethodInfo[] GetTargetMethods()
        {
            TypeInformationExtractor typeExtractor = new TypeInformationExtractor(_target);

            return typeExtractor.GetMethods();
        }
    }
}