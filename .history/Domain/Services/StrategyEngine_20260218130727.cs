using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlgoDDD.MarketData.Domain.Entities;
using AlgoDDD.Strategy.Domain.Entities;

namespace AlgoDDD.Strategy.Domain.Services
{
    /// <summary>
    /// StrategyEngine orchestrates the execution of trading strategies.
    /// It fetches market data, computes indicators, and delegates signal evaluation
    /// to the StrategyEntity based on the configured strategy type.
    /// </summary>
    public class StrategyEngine
    {
        private readonly IMarketDataProvider _marketDataProvider;

        public StrategyEngine(IMarketDataProvider marketDataProvider)
        {
            _marketDataProvider = marketDataProvider;
        }

        /// <summary>
        /// Executes a strategy against the latest market data for all symbols in the strategy.
        /// </summary>
        /// <param name="strategyEntity">The strategy entity containing configuration and type.</param>
        /// <returns>A list of signals (Buy, Sell, Hold) generated for each symbol.</returns>
        public async Task<List<Signal>> ExecuteStrategyAsync(StrategyEntity strategyEntity)
        {
            var signals = new List<Signal>();

            foreach (var symbol in strategyEntity.Symbols)
            {
                var bars = await _marketDataProvider.GetHistoricalBarsAsync(symbol, 50);
                var latestBar = bars[^1];

                switch (strategyEntity.StrategyType)
                {
                    case "SMA":
                        var shortMA = ComputeSMA(bars, 10);
                        var longMA = ComputeSMA(bars, 30);
                        signals.Add(strategyEntity.Evaluate(latestBar, shortMA: shortMA, longMA: longMA));
                        break;

                    case "MeanReversion":
                        var mean = ComputeMean(bars);
                        var threshold = ComputeStdDev(bars);
                        signals.Add(strategyEntity.Evaluate(latestBar, mean: mean, threshold: threshold));
                        break;

                    case "Momentum":
                        signals.Add(strategyEntity.Evaluate(latestBar));
                        break;

                    case "BollingerBands":
                        var bbMean = ComputeSMA(bars, 20);
                        var bbStdDev = ComputeStdDev(bars);
                        var upperBand = bbMean + 2 * bbStdDev;
                        var lowerBand = bbMean - 2 * bbStdDev;
                        signals.Add(strategyEntity.Evaluate(latestBar, upperBand: upperBand, lowerBand: lowerBand));
                        break;

                    default:
                        signals.Add(new Signal(SignalType.Hold, DateTime.UtcNow));
                        break;
                }
            }

            return signals;
        }

        /// <summary>
        /// Computes the Simple Moving Average (SMA) for the given period.
        /// </summary>
        private decimal ComputeSMA(List<PriceBar> bars, int period)
        {
            if (bars.Count < period) return 0;
            decimal sum = 0;
            for (int i = bars.Count - period; i < bars.Count; i++)
                sum += bars[i].Close;
            return sum / period;
        }

        /// <summary>
        /// Computes the mean (average) closing price across all bars.
        /// </summary>
        private decimal ComputeMean(List<PriceBar> bars)
        {
            decimal sum = 0;
            foreach (var bar in bars) sum += bar.Close;
            return sum / bars.Count;
        }

        /// <summary>
        /// Computes the standard deviation of closing prices across all bars.
        /// </summary>
        private decimal ComputeStdDev(List<PriceBar> bars)
        {
            var mean = ComputeMean(bars);
            decimal variance = 0;
            foreach (var bar in bars)
                variance += (bar.Close - mean) * (bar.Close - mean);
            return (decimal)Math.Sqrt((double)(variance / bars.Count));
        }
    }
}
