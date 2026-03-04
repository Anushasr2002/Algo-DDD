// File: C:\Algo-DDD\CoreContexts\Strategy\src\AlgoDDD.Strategy\Infrastructure\Mappers\StrategyMappers.cs
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.ValueObjects;
using AlgoDDD.Strategy.Infrastructure.Models;

namespace AlgoDDD.Strategy.Infrastructure.Mappers
{
    public static class StrategyMappers
    {
        public static StrategyEntity ToDomain(StrategyDataModel model)
        {
            if (model == null) return null;

            // Create StrategyId from the Guid
            var strategyId = StrategyId.From(model.Id);
            
            // Create StrategyType (assuming it's stored as string in the model)
            var strategyType = new StrategyType(model.Type);
            
            // Create the entity using the constructor
            var entity = new StrategyEntity(strategyId, strategyType, model.Parameters ?? new List<string>());
            
            // Set additional properties if they have public setters
            // If these properties don't exist, remove these lines
            // entity.Name = model.Name;
            // entity.Description = model.Description;
            
            return entity;
        }
        
        public static StrategyDataModel ToInfrastructure(StrategyEntity entity)
        {
            if (entity == null) return null;
            
            return new StrategyDataModel
            {
                Id = entity.Id.Value,  // Convert StrategyId to Guid
                Name = entity.Name,
                Description = entity.Description,
                Type = entity.Type.ToString(),
                Parameters = entity.Parameters,
                // If these properties don't exist in StrategyEntity, remove them
                // IsActive = true,
                // CreatedAt = DateTime.UtcNow,
                // UpdatedAt = null
            };
        }
    }
}