using System;

namespace Logos.Domain.Core
{
    public sealed class Username
    {
        readonly string _value;

        public Username(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("value");
            }

            _value = value;
        }

        public string Value
        {
            get
            {
                return _value;
            }
        }
    }
}