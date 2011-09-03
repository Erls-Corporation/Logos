using System;

namespace Logos.Domain.Core
{
    public sealed class ApiToken
    {
        readonly string _value;

        public ApiToken(string value)
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