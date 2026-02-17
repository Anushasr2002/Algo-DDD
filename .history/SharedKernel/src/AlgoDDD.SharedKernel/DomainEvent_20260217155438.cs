using System;

namespace AlgoDDD.SharedKernel.Domain.BaseClasses
{
    /// <summary>
    /// Base class for all domain events in the system.
    /// Provides timestamp and optional correlation identifiers.
    /// </summary>
    public abstract class DomainEvent
    {
        /// <summary>
        /// When the event occurred (UTC).
        /// </summary>
        public DateTime OccurredOn { get; protected set; } = DateTime.UtcNow;

        /// <summary>
        /// Unique identifier for this event instance.
        /// Useful for deduplication or event sourcing.
        /// </summary>
        public Guid EventId { get; private set; } = Guid.NewGuid();

        /// <summary>
        /// Optional correlation ID to link related events across contexts.
        /// </summary>
        public string? CorrelationId { get; protected set; }

        protected DomainEvent(string? correlationId = null)
        {
            CorrelationId = correlationId;
        }
    }
}
