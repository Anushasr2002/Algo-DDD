using System;
using AlgoDDD.SharedKernel.Domain.Events;

namespace AlgoDDD.SharedKernel.Domain.BaseClasses
{
    /// <summary>
    /// Base class for all domain events.
    /// Provides timestamp and unique identifier.
    /// </summary>
    public abstract class DomainEvent : IDomainEvent
    {
        public DateTime OccurredOn { get; protected set; } = DateTime.UtcNow;
        public Guid EventId { get; private set; } = Guid.NewGuid();

        protected DomainEvent() { }
    }
}
