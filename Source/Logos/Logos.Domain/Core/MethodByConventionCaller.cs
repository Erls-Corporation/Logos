using System.Reflection;
using System;
namespace Logos.Domain.Core
{
    public sealed class MethodByConventionCaller
    {
        readonly object _target;

        public MethodByConventionCaller(object target)
        {
            _target = target;
        }

        public void CallByConvention(object parameter)
        {
            MethodCollector collector = new MethodCollector(_target);

            MethodInfo firstMethodMatch = collector.CollectFirstMatch(new MethodQueryCriteria(parameter));

            MethodCaller caller = new MethodCaller(firstMethodMatch);
            caller.Call(_target, parameter);
        }
    }
}