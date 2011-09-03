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
            where T : IDomainEvent
        {
            foreach (T currentChange in changes)
            {
                Apply(currentChange);
            }
        }

        void Apply<T>(T change)
            where T : IDomainEvent
        {
            _methodCaller.CallByConvention(change);
        }
    }
}