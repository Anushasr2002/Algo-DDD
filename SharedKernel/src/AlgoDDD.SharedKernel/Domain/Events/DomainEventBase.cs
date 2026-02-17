using System;

namespace AlgoDDD.SharedKernel.Domain.Events;

public abstract class DomainEventBase : IDomainEvent
{
    public DateTime OccurredOn { get; }
    public string EventType => GetType().Name;
    
    protected DomainEventBase()
    {
        OccurredOn = DateTime.UtcNow;
    }
}
