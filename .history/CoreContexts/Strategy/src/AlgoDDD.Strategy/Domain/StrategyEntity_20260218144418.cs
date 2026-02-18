using System;
using System.Collections.Generic;
using AlgoDDD.MarketData.Domain.Entities;

namespace AlgoDDD.Strategy.Domain.Entities
{
    /// <summary>
    /// Represents a trading strategy entity that can evaluate signals
    /// and maintain a history of generated signals.
    /// </summary>
    public class StrategyEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string StrategyType { get; private set; }
        public string Description { get; private set; }

        private readonly List<Signal> _signals = new();
        public IReadOnlyCollection<Signal> Signals => _signals.AsReadOnly();

        public StrategyEntity(Guid id, string name, string strategyType, string description = "")
        {
            Id = id;
            Name = name;
            StrategyType = strategyType;
            Description = description;
        }

        /// <summary>
        /// Evaluates a trading signal based on the configured strategy type.
        /// </summary>
        /// <param name="marketData">Latest market data bar (OHLCV).</param>
        /// <param name="shortMA">Short-term moving average (used in SMA crossover).</param>
        /// <param name="longMA">Long-term moving average (used in SMA crossover).</param>
        /// <param name="mean">Mean price level (used in Mean Reversion).</param>
        /// <param name="threshold">Threshold or standard deviation (used in Mean Reversion).</param>
        /// <param name="upperBand">Upper Bollinger Band (mean + 2*stddev).</param>
        /// <param name="lowerBand">Lower Bollinger Band (mean - 2*stddev).</param>
        /// <returns>A Signal (Buy, Sell, or Hold).</returns>
        public Signal Evaluate(
            PriceBar marketData,
            decimal shortMA = 0,
            decimal longMA = 0,
            decimal mean = 0,
            decimal threshold = 0,
            decimal upperBand = 0,
            decimal lowerBand = 0)
        {
            Signal result;

            switch (StrategyType)
            {
                case "SMA":
                    result = shortMA > longMA
                        ? new Signal(SignalType.Buy, DateTime.UtcNow)
                        : shortMA < longMA
                            ? new Signal(SignalType.Sell, DateTime.UtcNow)
                            : new Signal(SignalType.Hold, DateTime.UtcNow);
                    break;

                case "MeanReversion":
                    result = marketData.Close > mean + threshold
                        ? new Signal(SignalType.Sell, DateTime.UtcNow)
                        : marketData.Close < mean - threshold
                            ? new Signal(SignalType.Buy, DateTime.UtcNow)
                            : new Signal(SignalType.Hold, DateTime.UtcNow);
                    break;

                case "Momentum":
                    result = marketData.Close > marketData.Open * 1.02m
                        ? new Signal(SignalType.Buy, DateTime.UtcNow)
                        : marketData.Close < marketData.Open * 0.98m
                            ? new Signal(SignalType.Sell, DateTime.UtcNow)
                            : new Signal(SignalType.Hold, DateTime.UtcNow);
                    break;

                case "BollingerBands":
                    result = marketData.Close > upperBand
                        ? new Signal(SignalType.Buy, DateTime.UtcNow)
                        : marketData.Close < lowerBand
                            ? new Signal(SignalType.Sell, DateTime.UtcNow)
                            : new Signal(SignalType.Hold, DateTime.UtcNow);
                    break;

                default:
                    result = new Signal(SignalType.Hold, DateTime.UtcNow);
                    break;
            }

            AddSignal(result);
            return result;
        }

        /// <summary>
        /// Adds a signal to the strategy's history.
        /// </summary>
        public void AddSignal(Signal signal) => _signals.Add(signal);
    }
}
