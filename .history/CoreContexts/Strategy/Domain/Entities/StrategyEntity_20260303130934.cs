using System;
using AlgoDDD.Strategy.Domain.ValueObjects;


namespace AlgoDDD.Strategy.Domain.Entities
{
    public class StrategyEntity
    {
        public StrategyId Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Type { get; private set; }
        public string TimeFrame { get; private set; }
        public decimal MaxPositionSize { get; private set; }
        public decimal StopLoss { get; private set; }
        public decimal TakeProfit { get; private set; }

        // ✅ Add a property to hold the current signal
        public string Signal { get; private set; }

        // ✅ Add the UpdateSignal method
        public void UpdateSignal(string newSignal)
        {
            if (string.IsNullOrWhiteSpace(newSignal))
                throw new ArgumentException("Signal cannot be empty.", nameof(newSignal));

            Signal = newSignal;
        }
    }
}
