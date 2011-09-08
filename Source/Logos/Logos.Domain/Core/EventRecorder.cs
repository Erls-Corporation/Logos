using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logos.Domain.Events;

namespace Logos.Domain.Core
{
    internal sealed class EventRecorder
    {
        readonly List<DomainEvent> _changes;

        public EventRecorder()
        {
            _changes = new List<DomainEvent>();
        }

        public IList<DomainEvent> Changes
        {
            get
            {
                return new List<DomainEvent>(_changes);
            }
        }

        public void Record(DomainEvent change)
        {
            _changes.Add(change);
        }
    }
}
