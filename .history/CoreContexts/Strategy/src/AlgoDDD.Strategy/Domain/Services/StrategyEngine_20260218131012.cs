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

                    case "RSI":
                        var rsi = ComputeRSI(bars, 14);
                        signals.Add(strategyEntity.Evaluate(latestBar, rsi: rsi));
                        break;

                    default:
                        signals.Add(new Signal(SignalType.Hold, DateTime.UtcNow));
                        break;
                }
            }

            return signals;
        }

        private decimal ComputeSMA(List<PriceBar> bars, int period)
        {
            if (bars.Count < period) return 0;
            decimal sum = 0;
            for (int i = bars.Count - period; i < bars.Count; i++)
                sum += bars[i].Close;
            return sum / period;
        }

        private decimal ComputeMean(List<PriceBar> bars)
        {
            decimal sum = 0;
            foreach (var bar in bars) sum += bar.Close;
            return sum / bars.Count;
        }

        private decimal ComputeStdDev(List<PriceBar> bars)
        {
            var mean = ComputeMean(bars);
            decimal variance = 0;
            foreach (var bar in bars)
                variance += (bar.Close - mean) * (bar.Close - mean);
            return (decimal)Math.Sqrt((double)(variance / bars.Count));
        }

        /// <summary>
        /// Computes the Relative Strength Index (RSI) for the given period.
        /// RSI = 100 - (100 / (1 + RS)), where RS = AvgGain / AvgLoss.
        /// </summary>
        private decimal ComputeRSI(List<PriceBar> bars, int period)
        {
            if (bars.Count < period + 1) return 50; // neutral fallback

            decimal gains = 0, losses = 0;
            for (int i = bars.Count - period; i < bars.Count; i++)
            {
                var change = bars[i].Close - bars[i - 1].Close;
                if (change > 0) gains += change;
                else losses -= change; // losses are positive
            }

            decimal avgGain = gains / period;
            decimal avgLoss = losses / period;

            if (avgLoss == 0) return 100; // extreme overbought
            var rs = avgGain / avgLoss;
            var rsi = 100 - (100 / (1 + rs));
            return rsi;
        }
    }
}
