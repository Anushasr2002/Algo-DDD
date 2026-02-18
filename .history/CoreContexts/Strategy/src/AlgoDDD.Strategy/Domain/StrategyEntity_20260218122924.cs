using System;
using AlgoDDD.MarketData.Domain.Entities;

namespace AlgoDDD.Strategy.Domain.Entities
{
    /// <summary>
    /// Represents a trading strategy entity that can evaluate signals
    /// based on different strategy types (SMA, MeanReversion, Momentum, BollingerBands).
    /// </summary>
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
            switch (StrategyType)
            {
                /// <summary>
                /// SMA crossover strategy:
                /// - Buy when shortMA > longMA
                /// - Sell when shortMA < longMA
                /// - Hold otherwise
                /// </summary>
                case "SMA":
                    if (shortMA > longMA)
                        return new Signal(SignalType.Buy, DateTime.UtcNow);
                    else if (shortMA < longMA)
                        return new Signal(SignalType.Sell, DateTime.UtcNow);
                    break;

                /// <summary>
                /// Mean Reversion strategy:
                /// - Sell when price > mean + threshold
                /// - Buy when price < mean - threshold
                /// - Hold otherwise
                /// </summary>
                case "MeanReversion":
                    if (marketData.Close > mean + threshold)
                        return new Signal(SignalType.Sell, DateTime.UtcNow);
                    else if (marketData.Close < mean - threshold)
                        return new Signal(SignalType.Buy, DateTime.UtcNow);
                    break;

                /// <summary>
                /// Momentum strategy:
                /// - Buy when close > open * 1.02 (strong upward move)
                /// - Sell when close < open * 0.98 (strong downward move)
                /// - Hold otherwise
                /// </summary>
                case "Momentum":
                    if (marketData.Close > marketData.Open * 1.02m)
                        return new Signal(SignalType.Buy, DateTime.UtcNow);
                    else if (marketData.Close < marketData.Open * 0.98m)
                        return new Signal(SignalType.Sell, DateTime.UtcNow);
                    break;

                /// <summary>
                /// Bollinger Bands strategy:
                /// - Buy when price > upper band (volatility breakout upward)
                /// - Sell when price < lower band (volatility breakout downward)
                /// - Hold otherwise
                /// </summary>
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
