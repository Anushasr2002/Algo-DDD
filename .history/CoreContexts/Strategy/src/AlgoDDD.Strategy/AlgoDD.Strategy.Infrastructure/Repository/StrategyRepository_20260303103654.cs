using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.Interfaces;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Infrastructure.Repository
{
    public class StrategyRepository : IStrategyRepository
    {
        private readonly Dictionary<StrategyId, StrategyEntity> _store = new();

        public Task<StrategyEntity> AddAsync(StrategyEntity entity)
        {
            _store[entity.Id] = entity;
            return Task.FromResult(entity);
        }

        public Task<StrategyEntity?> GetByIdAsync(StrategyId id, CancellationToken cancellationToken = default)
        {
            _store.TryGetValue(id, out var entity);
            return Task.FromResult(entity);
        }

        public Task UpdateAsync(StrategyEntity entity, CancellationToken cancellationToken = default)
        {
            _store[entity.Id] = entity;
            return Task.CompletedTask;
        }
        public async Task SaveEvaluationResultAsync(EvaluationResult result, CancellationToken cancellationToken)
        {
        var entity = EvaluationResultMapper.ToEntity(result);
    _dbContext.EvaluationResults.Add(entity);
    await _dbContext.SaveChangesAsync(cancellationToken);
}

    }
}
