using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Logos.Domain.Core
{
    public sealed class MethodCaller
    {
        readonly MethodInfo _method;

        public MethodCaller(MethodInfo method)
        {
            _method = method;
        }

        public void Call(object target, object parameter)
        {
            _method.Invoke(target, new object[] { parameter });
        }
    }
}
