using System;
using System.Reflection;
namespace Logos.Domain.Core
{
    public sealed class TypeInformationExtractor
    {
        readonly object _value;

        public TypeInformationExtractor(object value)
        {
            _value = value;
        }

        public Type ExtractType()
        {
            return _value.GetType();
        }

        public MethodInfo[] GetMethods()
        {
            return ExtractType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }
    }
}