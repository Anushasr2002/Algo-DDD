using static AlgoDDD.SharedKernel.DomainEvents;
using System;
using AlgoDDD.SharedKernel.DomainEvents;

namespace AlgoDDD.Strategy.Domain.Events
{
    public class StrategyCreatedEvent : IDomainEvent
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

    public class StrategyExecutedEvent : IDomainEvent
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

    public class StrategyDeactivatedEvent : IDomainEvent
    {
        public Guid StrategyId { get; }

        public StrategyDeactivatedEvent(Guid strategyId)
        {
            OccurredOn = DateTime.UtcNow;
            StrategyId = strategyId;
        }
    }

    public class StrategyUpdatedEvent : IDomainEvent
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

    public class SignalGeneratedEvent : IDomainEvent
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

    public class StrategyActivatedEvent : IDomainEvent
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



