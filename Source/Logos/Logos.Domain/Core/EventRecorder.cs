using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logos.Domain.Events;

namespace Logos.Domain.Core
{
    internal sealed class EventRecorder
    {
        readonly List<IDomainEvent> _changes;

        public EventRecorder()
        {
            _changes = new List<IDomainEvent>();
        }

        public IList<IDomainEvent> Changes
        {
            get
            {
                return new List<IDomainEvent>(_changes);
            }
        }

        public void Record(IDomainEvent change)
        {
            _changes.Add(change);
        }
    }
}
