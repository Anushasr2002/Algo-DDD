using System;

namespace AlgoDDD.SharedKernel
{
    /// <summary>
    /// Base type for all domain events.
    /// </summary>
    public abstract class DomainEvent
    {
        public DateTime OccurredOn { get; private set; } = DateTime.UtcNow;
        public string EventType => GetType().Name;
    }
}
