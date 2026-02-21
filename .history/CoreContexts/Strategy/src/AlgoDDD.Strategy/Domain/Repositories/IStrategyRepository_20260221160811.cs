using System;
using System.Threading.Tasks;
using AlgoDDD.Strategy.Domain.Entities;

namespace AlgoDDD.Strategy.Domain.Repositories
{
    public interface IStrategyRepository
    {
        Task<StrategyEntity?> GetByIdAsync(Guid id);
        Task AddAsync(StrategyEntity strategy);
        Task UpdateAsync(StrategyEntity strategy);
        Task DeleteAsync(Guid id);
    }
}
