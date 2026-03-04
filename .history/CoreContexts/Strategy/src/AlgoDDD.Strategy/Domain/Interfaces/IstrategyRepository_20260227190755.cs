using System.Threading;
using System.Threading.Tasks;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Interfaces
{
    public interface IStrategyRepository
    {
        Task<StrategyEntity> AddAsync(StrategyEntity entity);
        Task UpdateAsync(StrategyEntity entity, CancellationToken cancellationToken = default);

        // ✅ Newly added method
        Task<StrategyEntity?> GetByIdAsync(StrategyId id, CancellationToken cancellationToken = default);
    }
}
