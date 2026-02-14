using AlgoDDD.Strategy.Domain.Aggregates;
using AlgoDDD.Strategy.Domain.Entities;

namespace AlgoDDD.Strategy.Domain.Interfaces;

public interface IStrategyRepository
{
    Task<Strategy?> GetByIdAsync(string id);
    Task<IEnumerable<Strategy>> GetAllAsync();
    Task<IEnumerable<Strategy>> GetActiveStrategiesAsync();
    Task<Strategy> AddAsync(Strategy strategy);
    Task UpdateAsync(Strategy strategy);
    Task DeleteAsync(string id);
    Task AddSignalAsync(Signal signal);
    Task<IEnumerable<Signal>> GetSignalsAsync(string strategyId, DateTime? from = null, DateTime? to = null);
}
