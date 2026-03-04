using System;
using System.Threading;
using System.Threading.Tasks;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Interfaces
{
    public interface IStrategyRepository
    {
        Task<StrategyEntity?> GetByIdAsync(StrategyId id, CancellationToken cancellationToken);

        // ✅ New method to persist evaluation results
        Task SaveEvaluationResultAsync(EvaluationResult result, CancellationToken cancellationToken);
    }
}
