using AlgoDDD.Strategy.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlgoDDD.Strategy.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for managing StrategyEntity persistence and retrieval.
    /// </summary>
    public interface IStrategyRepository
    {
        /// <summary>
        /// Retrieves a strategy by its unique identifier.
        /// </summary>
        Task<StrategyEntity?> GetByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all strategies.
        /// </summary>
        Task<IEnumerable<StrategyEntity>> GetAllAsync();

        /// <summary>
        /// Adds a new strategy to the repository.
        /// </summary>
        Task AddAsync(StrategyEntity strategy);

        /// <summary>
        /// Updates an existing strategy.
        /// </summary>
        Task UpdateAsync(StrategyEntity strategy);

        /// <summary>
        /// Removes a strategy by its unique identifier.
        /// </summary>
        Task DeleteAsync(Guid id);
    }
}
