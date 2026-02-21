using AlgoDDD.Strategy.Domain.Entities;

namespace AlgoDDD.Strategy.Domain.Services
{
    public interface IMarketDataProvider
    {
        Task<IEnumerable<Signal>> GetSignalsAsync(StrategyId strategyId, DateTime from, DateTime to);
    }
}
