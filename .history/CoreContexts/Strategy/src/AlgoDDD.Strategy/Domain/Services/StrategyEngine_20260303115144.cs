using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.ValueObjects;
using AlgoDDD.MarketData.Entities;

namespace AlgoDDD.Strategy.Domain.Services
{
    public class StrategyEngine
    {
        private readonly IMarketDataProvider _marketDataProvider;

        public StrategyEngine(IMarketDataProvider marketDataProvider)
        {
            _marketDataProvider = marketDataProvider 
                ?? throw new ArgumentNullException(nameof(marketDataProvider));
        }

        public async Task<List<EvaluationResult>> ExecuteStrategyAsync(StrategyEntity strategyEntity)
        {
            var results = new List<EvaluationResult>();

            foreach (var symbol in strategyEntity.Symbols)
            {
                var bars = await _marketDataProvider.GetHistoricalBarsAsync(symbol, 50);
                var latestBar = bars.Last(); // ✅ safer than bars[^1]

                switch (strategyEntity.Type.Value)
                {
                    case "SMA":
                        var shortMA = ComputeSMA(bars, 10);
                        var longMA = ComputeSMA(bars, 30);
                        results.Add(strategyEntity.Evaluate(latestBar, shortMA: shortMA, longMA: longMA));
                        break;

                    case "MeanReversion":
                        var mean = ComputeMean(bars);
                        var threshold = ComputeStdDev(bars);
                        results.Add(strategyEntity.Evaluate(latestBar, mean: mean, threshold: threshold));
                        break;

                    case "Momentum":
                        results.Add(strategyEntity.Evaluate(latestBar));
                        break;

                    case "BollingerBands":
                        var bbMean = ComputeSMA(bars, 20);
                        var bbStdDev = ComputeStdDev(bars);
                        var upperBand = bbMean + 2 * bbStdDev;
                        var lowerBand = bbMean - 2 * bbStdDev;
                        results.Add(strategyEntity.Evaluate(latestBar, upperBand: upperBand, lowerBand: lowerBand));
                        break;

                    case "RSI":
                        var rsi = ComputeRSI(bars, 14);
                        results.Add(strategyEntity.Evaluate(latestBar, rsi: rsi));
                        break;

                    default:
                        results.Add(new EvaluationResult(
                            strategyEntity.Id,
                            symbol,
                            SignalAction.Hold,
                            latestBar.Close,
                            "Default Hold",
                            DateTime.UtcNow));
                        break;
                }
            }

            return results;
        }

        public async Task<BacktestResult> BacktestStrategyAsync(
            StrategyEntity strategyEntity,
            DateTime from,
            DateTime to,
            decimal initialCapital)
        {
            var signals = await ExecuteStrategyAsync(strategyEntity);

            int tradesExecuted = signals.Count(s => s.Action != SignalAction.Hold); // ✅ replaces IsExecuted
            decimal profitLoss = signals.Sum(s => CalculatePnL(s));                 // ✅ replaces ProfitLoss

            return new BacktestResult(strategyEntity.Id, profitLoss, tradesExecuted);
        }

        // ✅ Helper method for PnL calculation
        private decimal CalculatePnL(EvaluationResult result)
        {
            return result.Action switch
            {
                SignalAction.Buy => +result.Price,
                SignalAction.Sell => -result.Price,
                _ => 0m
            };
        }

        // Indicator helpers (stubs for now)
        private decimal ComputeSMA(IEnumerable<Bar> bars, int period) => 0m;
        private decimal ComputeMean(IEnumerable<Bar> bars) => 0m;
        private decimal ComputeStdDev(IEnumerable<Bar> bars) => 0m;
        private decimal ComputeRSI(IEnumerable<Bar> bars, int period) => 0m;
    }
}
