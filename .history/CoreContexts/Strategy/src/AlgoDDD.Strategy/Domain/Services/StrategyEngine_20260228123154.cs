using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.ValueObjects;
using AlgoDDD.Strategy.Domain.Services;

public class StrategyEngine
{
    private readonly IMarketDataProvider _marketDataProvider;

    public StrategyEngine(IMarketDataProvider marketDataProvider)
    {
        _marketDataProvider = marketDataProvider 
            ?? throw new ArgumentNullException(nameof(marketDataProvider));
    }

    public async Task<List<Signal>> ExecuteStrategyAsync(StrategyEntity strategyEntity)
    {
        var signals = new List<Signal>();

        foreach (var symbol in strategyEntity.Symbols)
        {
            var bars = await _marketDataProvider.GetHistoricalBarsAsync(symbol, 50);
            var latestBar = bars[^1];

            switch (strategyEntity.Type.Value)
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

    public async Task<BacktestResult> BacktestStrategyAsync(
        StrategyId strategyId,
        DateTime from,
        DateTime to)
    {
        var signals = await _marketDataProvider.GetSignalsAsync(strategyId, from, to);
        int tradesExecuted = signals.Count(s => s.IsExecuted);
        decimal profitLoss = signals.Sum(s => s.ProfitLoss ?? 0);

        return new BacktestResult(strategyId, profitLoss, tradesExecuted);
    }

    // Indicator helpers...
}
