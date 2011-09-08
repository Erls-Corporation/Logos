using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logos.Domain.Events;

namespace Logos.Domain.Core
{
    public sealed class EventApplier
    {
        readonly MethodByConventionCaller _methodCaller;
        readonly EventRecorder _recorder;
        readonly object _target;

        public EventApplier(object target)
        {
            _methodCaller = new MethodByConventionCaller(target);
            _recorder = new EventRecorder();
            _target = target;
        }

        public void Apply<T>(T change)
            where T : DomainEvent
        {
            _recorder.Record(change);
            _methodCaller.CallByConvention(change);
        }

        public IEnumerable<DomainEvent> GetAppliedChanges()
        {
            return _recorder.Changes;
        }
    }
}
