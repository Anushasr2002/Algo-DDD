using System.Threading;
using System.Threading.Tasks;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.Interfaces;
using AlgoDDD.Strategy.Domain.ValueObjects;
using AlgoDDD.Strategy.Infrastructure.Entities;
using AlgoDDD.Strategy.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;


namespace AlgoDDD.Strategy.Infrastructure.Repository
{
    public class StrategyRepository : IStrategyRepository
    {
        private readonly StrategyDbContext _dbContext;

        public StrategyRepository(StrategyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StrategyEntity?> GetByIdAsync(StrategyId id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Strategies
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<StrategyEntity> AddAsync(StrategyEntity entity)
        {
            _dbContext.Strategies.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(StrategyEntity entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Strategies.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveEvaluationResultAsync(EvaluationResult result, CancellationToken cancellationToken)
        {
            var entity = EvaluationResultMapper.ToEntity(result);
            _dbContext.EvaluationResults.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
