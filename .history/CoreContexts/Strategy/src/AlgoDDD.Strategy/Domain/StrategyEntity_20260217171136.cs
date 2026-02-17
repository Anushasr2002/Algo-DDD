using AlgoDDD.SharedKernel.Domain.BaseClasses;
using AlgoDDD.SharedKernel.Domain.Events;
using System;
using System.Collections.Generic;



using AlgoDDD.Strategy.Domain.Events;

namespace AlgoDDD.Strategy.Domain
{
    // Renamed from Strategy to StrategyEntity to avoid namespace conflict
    public class StrategyEntity : AggregateRoot<Guid>
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public StrategyType Type { get; private set; }
        public StrategyParameters Parameters { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastExecutedAt { get; private set; }
        

        // For EF Core
        private StrategyEntity() { }

        public StrategyEntity(string name, string description, StrategyType type, StrategyParameters parameters)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Type = type;
            Parameters = parameters;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;

            // Add domain event
            AddDomainEvent(new StrategyCreatedEvent(Id, Name, CreatedAt));
        }

        public StrategyEntity(Guid id, string name, string description, string parameters, List<string> symbols, DateTime createdAt)
        {
        Id = id;
    Name = name;
    Description = description;
    Parameters = parameters;
    Symbols = symbols;
    CreatedAt = createdAt;
}


        public void Execute()
        {
            if (!IsActive)
                throw new InvalidOperationException("Cannot execute an inactive strategy");

            LastExecutedAt = DateTime.UtcNow;
            AddDomainEvent(new StrategyExecutedEvent(Id, LastExecutedAt.Value));
        }

        public void Deactivate()
        {
            IsActive = false;
            AddDomainEvent(new StrategyDeactivatedEvent(Id));
        }

        public void UpdateParameters(StrategyParameters newParameters)
        {
            Parameters = newParameters;
            AddDomainEvent(new StrategyUpdatedEvent(Id, Parameters));
        
        }
    }

    public enum StrategyType
    {
        MovingAverageCrossover,
        Rsi,
        BollingerBands,
        Custom
    }

    public class StrategyParameters
    {
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
    }

        
}





