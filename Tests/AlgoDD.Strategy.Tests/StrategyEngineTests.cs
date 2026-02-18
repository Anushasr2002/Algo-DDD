using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlgoDDD.MarketData.Domain.Entities;

namespace AlgoDDD.Strategy.Domain.Services
{
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
                        signals.Add(strategyEntity.Evaluate(latestBar, shortMA, longMA));
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

                        if (latestBar.Close > upperBand)
                            signals.Add(new Signal(SignalType.Buy, DateTime.UtcNow));
                        else if (latestBar.Close < lowerBand)
                            signals.Add(new Signal(SignalType.Sell, DateTime.UtcNow));
                        else
                            signals.Add(new Signal(SignalType.Hold, DateTime.UtcNow));
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
    }
}
