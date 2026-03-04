using System.Threading;
using System.Threading.Tasks;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.Interfaces;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Infrastructure.Repositories
{
    public class StrategyRepository : IStrategyRepository
    {
        private readonly StrategyDbContext _dbContext;

        public StrategyRepository(StrategyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StrategyEntity?> GetByIdAsync(StrategyId id, CancellationToken cancellationToken)
        {
            return await _dbContext.Strategies.FindAsync(new object[] { id.Value }, cancellationToken);
        }

        public async Task SaveEvaluationResultAsync(EvaluationResult result, CancellationToken cancellationToken)
        {
            // Map EvaluationResult to a persistence entity if needed
            var entity = new EvaluationResultEntity
            {
                StrategyId = result.StrategyId.Value,
                Symbol = result.Symbol,
                Action = result.Action.ToString(),
                Price = result.Price,
                Notes = result.Notes,
                Timestamp = result.Timestamp
            };

            _dbContext.EvaluationResults.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
