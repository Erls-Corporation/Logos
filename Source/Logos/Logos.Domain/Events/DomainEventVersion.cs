namespace Logos.Domain.Events
{
    public sealed class DomainEventVersion
    {
        readonly int _value;

        public DomainEventVersion()
            : this(-1)
        {
        }

        public DomainEventVersion(int value)
        {
            _value = value;
        }

        public int Value
        {
            get
            {
                return _value;
            }
        }

        public DomainEventVersion Increment()
        {
            int newVersion = _value + 1;
            return new DomainEventVersion(newVersion);
        }
    }
}