using AlgoDDD.Strategy.Domain;
using AlgoDDD.Strategy.Domain.Aggregates;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.Interfaces;
using AlgoDDD.MarketData.Domain.Entities;
using AlgoDDD.MarketData.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Services;

public class StrategyEngine
{
    private readonly IStrategyRepository _strategyRepository;
    private readonly IMarketDataService _marketDataService;

    public StrategyEngine(
        IStrategyRepository strategyRepository,
        IMarketDataService marketDataService)
    {
        _strategyRepository = strategyRepository;
        _marketDataService = marketDataService;
    }

    public async Task<List<Signal>> ExecuteAllStrategiesAsync()
    {
        var signals = new List<Signal>();
        var activeStrategies = await _strategyRepository.GetActiveStrategiesAsync();

        foreach (var StrategyEntity in activeStrategies)
        {
            var strategySignals = await ExecuteStrategyAsync(StrategyEntity);
            signals.AddRange(strategySignals);
        }

        return signals;
    }

    public async Task<List<Signal>> ExecuteStrategyAsync(StrategyEntity StrategyEntity)
    {
        var signals = new List<Signal>();

        foreach (var symbol in StrategyEntity.Symbols)
        {
            var stockSymbol = new StockSymbol(symbol, "NASDAQ", "USD");
            var marketData = await _marketDataService.GetLatestMarketDataAsync(stockSymbol);
            var historicalData = await _marketDataService.GetHistoricalDataAsync(stockSymbol, 100);

            if (marketData != null && historicalData.Any())
            {
                var signal = StrategyEntity.GenerateSignal(
                    stockSymbol,
                    marketData.CurrentPrice.Value,
                    historicalData.ToList()
                );

                if (signal != null)
                {
                    signals.Add(signal);
                    await _strategyRepository.AddSignalAsync(signal);
                }
            }
        }

        return signals;
    }

    public async Task<BacktestResult> BacktestStrategyAsync(
        StrategyEntity StrategyEntity,
        DateTime startDate,
        DateTime endDate,
        decimal initialCapital)
    {
        var result = new BacktestResult
        {
            StrategyName = StrategyEntity.Name,
            StartDate = startDate,
            EndDate = endDate,
            InitialCapital = initialCapital
        };

        var allSignals = new List<Signal>();
        var currentCapital = initialCapital;
        var currentPosition = 0m;

        // Get historical data for all symbols
        var historicalData = new Dictionary<string, List<MarketData>>();
        foreach (var symbol in StrategyEntity.Symbols)
        {
            var stockSymbol = new StockSymbol(symbol, "NASDAQ", "USD");
            var data = await _marketDataService.GetHistoricalDataAsync(stockSymbol, 500);
            historicalData[symbol] = data.ToList();
        }

        // Simulate trading day by day
        for (var date = startDate; date <= endDate; date = date.AddDays(1))
        {
            foreach (var symbol in StrategyEntity.Symbols)
            {
                var symbolData = historicalData[symbol]
                    .Where(d => d.Timestamp.Date == date)
                    .ToList();

                if (!symbolData.Any())
                    continue;

                var currentPrice = symbolData.Last().CurrentPrice.Value;
                var stockSymbol = new StockSymbol(symbol, "NASDAQ", "USD");

                // Generate signal based on historical data up to this date
                var historicalUpToDate = historicalData[symbol]
                    .Where(d => d.Timestamp <= date)
                    .ToList();

                var signal = StrategyEntity.GenerateSignal(stockSymbol, currentPrice, historicalUpToDate);

                if (signal != null)
                {
                    signal.Execute(currentPrice, currentCapital * 0.1m / currentPrice); // Use 10% of capital
                    allSignals.Add(signal);

                    if (signal.Type == SignalType.Buy || signal.Type == SignalType.StrongBuy)
                    {
                        currentPosition += signal.Quantity ?? 0;
                        currentCapital -= (signal.Quantity ?? 0) * currentPrice;
                    }
                    else if (signal.Type == SignalType.Sell || signal.Type == SignalType.StrongSell)
                    {
                        currentPosition -= signal.Quantity ?? 0;
                        currentCapital += (signal.Quantity ?? 0) * currentPrice;
                    }
                }
            }
        }

        // Close all positions at end date
        foreach (var symbol in StrategyEntity.Symbols)
        {
            var lastData = historicalData[symbol].Last();
            currentCapital += currentPosition * lastData.CurrentPrice.Value;
            currentPosition = 0;
        }

        result.FinalCapital = currentCapital;
        result.TotalReturn = ((currentCapital - initialCapital) / initialCapital) * 100;
        result.TotalSignals = allSignals.Count;
        result.WinningTrades = allSignals.Count(s => s.ProfitLoss > 0);
        result.LosingTrades = allSignals.Count(s => s.ProfitLoss < 0);
        result.Signals = allSignals;

        return result;
    }
}

public class BacktestResult
{
    public string StrategyName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal InitialCapital { get; set; }
    public decimal FinalCapital { get; set; }
    public decimal TotalReturn { get; set; }
    public int TotalSignals { get; set; }
    public int WinningTrades { get; set; }
    public int LosingTrades { get; set; }
    public List<Signal> Signals { get; set; } = new();
    
    public decimal WinRate => TotalSignals > 0 ? (decimal)WinningTrades / TotalSignals * 100 : 0;
    public decimal ProfitFactor => LosingTrades > 0 ? (decimal)WinningTrades / LosingTrades : 0;
}

