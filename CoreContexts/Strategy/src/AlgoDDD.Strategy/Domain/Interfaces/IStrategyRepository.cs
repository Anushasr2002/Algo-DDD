using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlgoDDD.Strategy.Domain;

namespace AlgoDDD.Strategy.Domain.Interfaces
{
    public interface IStrategyRepository
    {
        Task<StrategyEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<StrategyEntity>> GetAllAsync();
        Task<IEnumerable<StrategyEntity>> GetActiveAsync();
        Task AddAsync(StrategyEntity strategy);
        Task UpdateAsync(StrategyEntity strategy);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}
