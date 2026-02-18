using System;
using AlgoDDD.MarketData.Domain.Entities;

namespace AlgoDDD.Strategy.Domain.Entities
{
    public class StrategyEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string StrategyType { get; private set; }

        public StrategyEntity(Guid id, string name, string strategyType)
        {
            Id = id;
            Name = name;
            StrategyType = strategyType;
        }

        /// <summary>
        /// Hybrid signal evaluation method supporting multiple strategies.
        /// </summary>
        public Signal Evaluate(
            PriceBar marketData,
            decimal shortMA = 0,
            decimal longMA = 0,
            decimal mean = 0,
            decimal threshold = 0,
            decimal upperBand = 0,
            decimal lowerBand = 0)
        {
            switch (StrategyType)
            {
                case "SMA":
                    if (shortMA > longMA)
                        return new Signal(SignalType.Buy, DateTime.UtcNow);
                    else if (shortMA < longMA)
                        return new Signal(SignalType.Sell, DateTime.UtcNow);
                    break;

                case "MeanReversion":
                    if (marketData.Close > mean + threshold)
                        return new Signal(SignalType.Sell, DateTime.UtcNow);
                    else if (marketData.Close < mean - threshold)
                        return new Signal(SignalType.Buy, DateTime.UtcNow);
                    break;

                case "Momentum":
                    if (marketData.Close > marketData.Open * 1.02m)
                        return new Signal(SignalType.Buy, DateTime.UtcNow);
                    else if (marketData.Close < marketData.Open * 0.98m)
                        return new Signal(SignalType.Sell, DateTime.UtcNow);
                    break;

                case "BollingerBands":
                    if (marketData.Close > upperBand)
                        return new Signal(SignalType.Buy, DateTime.UtcNow);
                    else if (marketData.Close < lowerBand)
                        return new Signal(SignalType.Sell, DateTime.UtcNow);
                    else
                        return new Signal(SignalType.Hold, DateTime.UtcNow);
                    
                default:
                    return new Signal(SignalType.Hold, DateTime.UtcNow);
            }

            return new Signal(SignalType.Hold, DateTime.UtcNow);
        }
    }
}
