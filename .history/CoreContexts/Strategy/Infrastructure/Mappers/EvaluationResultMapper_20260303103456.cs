using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.ValueObjects;
using AlgoDDD.Strategy.Infrastructure.Entities;

namespace AlgoDDD.Strategy.Infrastructure.Mappers
{
    public static class EvaluationResultMapper
    {
        // ✅ Domain → Persistence
        public static EvaluationResultEntity ToEntity(EvaluationResult result)
        {
            return new EvaluationResultEntity
            {
                StrategyId = result.StrategyId.Value,
                Symbol = result.Symbol,
                Action = result.Action.ToString(),
                Price = result.Price,
                Notes = result.Notes,
                Timestamp = result.Timestamp
            };
        }

        // ✅ Persistence → Domain
        public static EvaluationResult ToDomain(EvaluationResultEntity entity)
        {
            return new EvaluationResult(
                new StrategyId(entity.StrategyId),
                entity.Symbol,
                Enum.Parse<SignalAction>(entity.Action),
                entity.Price,
                entity.Notes,
                entity.Timestamp
            );
        }
    }
}
