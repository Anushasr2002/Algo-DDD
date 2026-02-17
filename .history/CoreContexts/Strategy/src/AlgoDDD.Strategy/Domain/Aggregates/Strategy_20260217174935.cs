using System;
using System.Collections.Generic;
using AlgoDDD.SharedKernel.Domain.BaseClasses;
using AlgoDDD.SharedKernel.Domain.Events;
using AlgoDDD.Strategy.Domain.Events;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.MarketData.Domain.Entities;
using AlgoDDD.MarketData.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Aggregates
{
    /// <summary>
    /// Aggregate root representing a trading strategy.
    /// </summary>
    public class Strategy : AggregateRoot<string>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public StrategyType Type { get; private set; }
        public Dictionary<string, object> Parameters { get; private set; }
        public List<string> Symbols { get; private set; }
        public TimeFrame TimeFrame { get; private set; }
        public bool IsActive { get; private set; }
        public decimal? MaxPositionSize { get; private set; }
        public decimal? StopLoss { get; private set; }
        public decimal? TakeProfit { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastExecutedAt { get; private set; }
        public int TotalSignals { get; private set; }
        public int SuccessfulTrades { get; private set; }
        public decimal TotalReturn { get; private set; }

        private Strategy() : base(string.Empty) { } // For EF Core

        public Strategy(
            string name,
            string description,
            StrategyType type,
            Dictionary<string, object> parameters,
            List<string> symbols,
            TimeFrame timeFrame) : base(Guid.NewGuid().ToString())
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Type = type;
            Parameters = parameters ?? new Dictionary<string, object>();
            Symbols = symbols ?? new List<string>();
            TimeFrame = timeFrame;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            TotalSignals = 0;
            SuccessfulTrades = 0;
            TotalReturn = 0;

            AddDomainEvent(new StrategyCreatedEvent(this));
        }

        public void Activate()
        {
            IsActive = true;
            AddDomainEvent(new StrategyActivatedEvent(this));
        }

        public void Deactivate()
        {
            IsActive = false;
            AddDomainEvent(new StrategyDeactivatedEvent(this));
        }

        public void Execute()
        {
            LastExecutedAt = DateTime.UtcNow;
            AddDomainEvent(new StrategyExecutedEvent(this));
        }

        public void UpdatePerformance(bool success, decimal profitLoss)
        {
            if (success)
                SuccessfulTrades++;

            TotalReturn += profitLoss;
        }
    }

    /// <summary>
    /// Defines the type of strategy.
    /// </summary>
    public enum StrategyType
    {
        Momentum,
        MeanReversion,
        Arbitrage,
        Custom
    }
}

