using System.Threading;
using System.Threading.Tasks;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Interfaces
{
    /// <summary>
    /// Defines the contract for executing a trading strategy.
    /// </summary>
    public interface IStrategyExecutor
    {
        /// <summary>
        /// Executes the strategy identified by the given StrategyId.
        /// </summary>
        /// <param name="strategyId">The unique identifier of the strategy to execute.</param>
        /// <param name="cancellationToken">Token to cancel the execution.</param>
        /// <returns>True if execution succeeded, false otherwise.</returns>
        Task<bool> ExecuteAsync(StrategyId strategyId, CancellationToken cancellationToken);
    }
}
