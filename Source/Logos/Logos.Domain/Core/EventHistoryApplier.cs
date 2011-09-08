using System.Collections.Generic;
using Logos.Domain.Events;
namespace Logos.Domain.Core
{
    public sealed class EventHistoryApplier
    {
        readonly MethodByConventionCaller _methodCaller;
        readonly object _target;

        public EventHistoryApplier(object target)
        {
            _methodCaller = new MethodByConventionCaller(target);
            _target = target;
        }

        public void Apply<T>(IEnumerable<T> changes)
            where T : DomainEvent
        {
            foreach (T currentChange in changes)
            {
                Apply(currentChange);
            }
        }

        void Apply<T>(T change)
            where T : DomainEvent
        {
            _methodCaller.CallByConvention(change);
        }
    }
}