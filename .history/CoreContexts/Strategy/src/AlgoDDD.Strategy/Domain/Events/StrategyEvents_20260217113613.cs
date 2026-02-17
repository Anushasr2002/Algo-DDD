using static AlgoDDD.SharedKernel.DomainEvents;
using System;

namespace AlgoDDD.Strategy.Domain.Events
{
    public class StrategyCreatedEvent : DomainEvent
    {
        public Guid StrategyId { get; }
        public string Name { get; }
        public DateTime CreatedAt { get; }

        public StrategyCreatedEvent(Guid strategyId, string name, DateTime createdAt)
        {
            OccurredOn = DateTime.UtcNow;
            StrategyId = strategyId;
            Name = name;
            CreatedAt = createdAt;
        }
    }

    public class StrategyExecutedEvent : DomainEvent
    {
        public Guid StrategyId { get; }
        public DateTime ExecutedAt { get; }

        public StrategyExecutedEvent(Guid strategyId, DateTime executedAt)
        {
            OccurredOn = DateTime.UtcNow;
            StrategyId = strategyId;
            ExecutedAt = executedAt;
        }
    }

    public class StrategyDeactivatedEvent : DomainEvent
    {
        public Guid StrategyId { get; }

        public StrategyDeactivatedEvent(Guid strategyId)
        {
            OccurredOn = DateTime.UtcNow;
            StrategyId = strategyId;
        }
    }

    public class StrategyUpdatedEvent : DomainEvent
    {
        public Guid StrategyId { get; }
        public object Parameters { get; }

        public StrategyUpdatedEvent(Guid strategyId, object parameters)
        {
            OccurredOn = DateTime.UtcNow;
            StrategyId = strategyId;
            Parameters = parameters;
        }
    }

    public class SignalGeneratedEvent : DomainEvent
    {
        public Guid StrategyId { get; }
        public object Signal { get; }

        public SignalGeneratedEvent(Guid strategyId, object signal)
        {
            OccurredOn = DateTime.UtcNow;
            StrategyId = strategyId;
            Signal = signal;
        }
    }

    public class StrategyActivatedEvent : DomainEvent
    {
        public Guid StrategyId { get; }
        public DateTime ActivatedAt { get; }

        public StrategyActivatedEvent(Guid strategyId, DateTime activatedAt)
        {
            OccurredOn = DateTime.UtcNow;
            StrategyId = strategyId;
            ActivatedAt = activatedAt;
        }
    }
}
F


