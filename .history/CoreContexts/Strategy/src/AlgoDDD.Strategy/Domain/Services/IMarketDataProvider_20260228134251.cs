namespace AlgoDDD.Strategy.Domain.Services {

    public interface IMarketDataProvider
{
    Task<IEnumerable<Bar>> GetHistoricalBarsAsync(string symbol, int lookback);
    Task<List<Signal>> GetSignalsAsync(StrategyId strategyId, DateTime from, DateTime to);
}

}

