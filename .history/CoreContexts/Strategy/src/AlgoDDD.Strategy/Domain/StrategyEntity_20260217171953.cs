using System;
using System.Collections.Generic;
using AlgoDDD.SharedKernel.Domain.BaseClasses;
using AlgoDDD.SharedKernel.Domain.Events;
using AlgoDDD.Strategy.Domain.Events;

namespace AlgoDDD.Strategy.Domain
{
    /// <summary>
    /// Represents a trading strategy entity in the domain.
    /// </summary>
    public class StrategyEntity : Entity<Guid>
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string Parameters { get; private set; } = string.Empty;
        public List<string> Symbols { get; private set; } = new();
        public DateTime CreatedAt { get; private set; }

        // Constructor matching CreateStrategyCommand (6 arguments)
        public StrategyEntity(Guid id, string name, string description, string parameters, List<string> symbols, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Description = description;
            Parameters = parameters;
            Symbols = symbols;
            CreatedAt = createdAt;

            // Raise domain event
            AddDomainEvent(new StrategyCreatedEvent(id, name, createdAt));
        }

        // Example method to deactivate strategy
        public void Deactivate()
        {
            AddDomainEvent(new StrategyDeactivatedEvent(Id, DateTime.UtcNow));
        }

        // Example method to execute strategy
        public void Execute()
        {
            AddDomainEvent(new StrategyExecutedEvent(Id, DateTime.UtcNow));
        }

        // Example method to update strategy
        public void Update(string description, string parameters, List<string> symbols)
        {
            Description = description;
            Parameters = parameters;
            Symbols = symbols;

            AddDomainEvent(new StrategyUpdatedEvent(Id, DateTime.UtcNow));
        }

        // Example signal generation method
        public Signal GenerateSignal(MarketData data)
        {
            // Simplified domain logic: always return Buy
            return new Signal(SignalType.Buy, DateTime.UtcNow);
        }

        // Helper to add domain events
        private void AddDomainEvent(IDomainEvent domainEvent)
        {
            DomainEvents.Raise(domainEvent);
        }
    }

    // Example Signal class
    public class Signal
    {
        public SignalType Type { get; }
        public DateTime Timestamp { get; }

        public Signal(SignalType type, DateTime timestamp)
        {
            Type = type;
            Timestamp = timestamp;
        }
    }

    // Example SignalType enum
    public enum SignalType
    {
        Buy,
        Sell,
        Hold
    }
}
