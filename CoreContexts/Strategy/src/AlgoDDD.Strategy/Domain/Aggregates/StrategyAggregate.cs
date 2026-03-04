using System;
using System.Collections.Generic;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Aggregates
{
    public class StrategyAggregate
    {
        public StrategyId Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Type { get; private set; }
        public string TimeFrame { get; private set; }
        public decimal MaxPositionSize { get; private set; }
        public decimal StopLoss { get; private set; }
        public decimal TakeProfit { get; private set; }

        private readonly List<string> _signals = new();
        public IReadOnlyCollection<string> Signals => _signals.AsReadOnly();

        public StrategyAggregate(StrategyId id, string name, string type, string timeFrame)
        {
            Id = id;
            Name = name;
            Type = type;
            TimeFrame = timeFrame;
        }

        public void UpdateSignal(string newSignal)
        {
            if (string.IsNullOrWhiteSpace(newSignal))
                throw new ArgumentException("Signal cannot be empty.", nameof(newSignal));

            _signals.Add(newSignal);
        }
    }
}
