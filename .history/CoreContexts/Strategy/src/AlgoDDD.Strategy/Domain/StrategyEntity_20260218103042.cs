using System;
using System.Collections.Generic;
using AlgoDDD.SharedKernel.Domain.BaseClasses;
using AlgoDDD.SharedKernel.Domain.Events;
using AlgoDDD.Strategy.Domain.Events;
using AlgoDDD.MarketData.Domain;              // For PriceBar
using AlgoDDD.MarketData.Domain.Entities;    // For other market data entities

namespace AlgoDDD.Strategy.Domain
{
    /// <summary>
    /// Represents a trading strategy aggregate root in the domain.
    /// </summary>
    public class StrategyEntity : Entity<Guid>
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string Parameters { get; private set; } = string.Empty;
        public List<string> Symbols { get; private set; } = new();
        public DateTime CreatedAt { get; private set; }

        // Constructor matching CreateStrategyCommand
        public StrategyEntity(Guid id, string name, string description, string parameters, List<string> symbols, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Description = description;
            Parameters = parameters;
            Symbols = symbols;
            CreatedAt = createdAt;

            AddDomainEvent(new StrategyCreatedEvent(id, name, createdAt));
        }

        // Lifecycle methods
        public void Deactivate() => AddDomainEvent(new StrategyDeactivatedEvent(Id, DateTime.UtcNow));
        public void Execute() => AddDomainEvent(new StrategyExecutedEvent(Id, DateTime.UtcNow));
        public void Update(string description, string parameters, List<string> symbols)
        {
            Description = description;
            Parameters = parameters;
            Symbols = symbols;
            AddDomainEvent(new StrategyUpdatedEvent(Id, DateTime.UtcNow));
        }

        // Hybrid signal generation method
        public Signal Evaluate(PriceBar marketData)
        {
            // Example: SMA crossover logic simplified to Open vs Close
            if (marketData.Close > marketData.Open)
            {
                return new Signal(SignalType.Buy, DateTime.UtcNow);
            }
            else
            {
                return new Signal(SignalType.Sell, DateTime.UtcNow);
            }
        }

        // Helper to add domain events
        private void AddDomainEvent(IDomainEvent domainEvent) => DomainEvents.Raise(domainEvent);
    }

    // Signal class
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

    // SignalType enum
    public enum SignalType
    {
        Buy,
        Sell,
        Hold
    }
}
