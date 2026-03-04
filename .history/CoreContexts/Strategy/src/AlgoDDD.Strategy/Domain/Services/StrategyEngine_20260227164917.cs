using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlgoDDD.MarketData.Domain.Entities;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Services {
{
    /// <summary>
    /// StrategyEngine orchestrates execution and backtesting of trading strategies.
    /// It fetches market data, computes indicators, and delegates signal evaluation
    /// to the StrategyEntity based on the configured strategy type.
    /// </summary>
    public class StrategyEngine
    {
        private readonly IMarketDataProvider _marketDataProvider;

        public StrategyEngine(IMarketDataProvider marketDataProvider)
        {
            _marketDataProvider = marketDataProvider 
                ?? throw new ArgumentNullException(nameof(marketDataProvider));
        }

        /// <summary>
        /// Executes a strategy live and produces signals for each symbol.
        /// </summary>
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
                        signals.Add(new Signal(strategyEntity.Id, symbol, SignalType.Hold, latestBar.Close, "Default Hold", DateTime.UtcNow));
                        break;
                }
            }

            return signals;
        }

        /// <summary>
        /// Runs a backtest for the given strategy between two dates.
        /// </summary>
        public async Task<BacktestResult> RunBacktestAsync(
            StrategyId strategyId,
            DateTime from,
            DateTime to)
        {
            var signals = await _marketDataProvider.GetSignalsAsync(strategyId, from, to);

            int tradesExecuted = signals.Count(s => s.IsExecuted);
            decimal profitLoss = signals.Sum(s => s.ProfitLoss ?? 0);

            return new BacktestResult(strategyId, profitLoss, tradesExecuted);
        }

        // --- Indicator helpers ---
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
            decimal sum = bars.Sum(b => b.Close);
            return bars.Count == 0 ? 0 : sum / bars.Count;
        }

        private decimal ComputeStdDev(List<PriceBar> bars)
        {
            var mean = ComputeMean(bars);
            decimal variance = bars.Sum(b => (b.Close - mean) * (b.Close - mean));
            return bars.Count == 0 ? 0 : (decimal)Math.Sqrt((double)(variance / bars.Count));
        }

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
            return 100 - (100 / (1 + rs));
        }
    }
}
}
