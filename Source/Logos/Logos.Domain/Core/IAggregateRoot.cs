using System.Collections;
using Logos.Domain.Events;
using System.Collections.Generic;
using System;
namespace Logos.Domain.Core
{
    public interface IAggregateRoot
    {
        Guid Id { get; }

        IEnumerable<DomainEvent> GetUncommittedChanges();
        void MarkChangesAsCommitted();
        void LoadFromHistory(IEnumerable<DomainEvent> changes);
    }
}