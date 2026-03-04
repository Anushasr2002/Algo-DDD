using System.Threading;
using System.Threading.Tasks;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Interfaces
{
    public interface IStrategyRepository
    {
        Task<StrategyEntity?> GetByIdAsync(StrategyId id, CancellationToken cancellationToken = default);
        Task<StrategyEntity> AddAsync(StrategyEntity entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(StrategyEntity entity, CancellationToken cancellationToken = default);
        Task SaveEvaluationResultAsync(EvaluationResult result, CancellationToken cancellationToken = default);
    }
}