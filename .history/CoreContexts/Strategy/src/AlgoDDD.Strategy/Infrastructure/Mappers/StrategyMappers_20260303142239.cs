using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Infrastructure.Models;

namespace AlgoDDD.Strategy.Infrastructure.Mappers
{
    public static class StrategyMappers
    {
        public static StrategyEntity ToDomain(StrategyDataModel model)
        {
            if (model == null) return null;
            
            return new StrategyEntity
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                IsActive = model.IsActive,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
                // Add other properties as needed
            };
        }
        
        public static StrategyDataModel ToInfrastructure(StrategyEntity entity)
        {
            if (entity == null) return null;
            
            return new StrategyDataModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
                // Add other properties as needed
            };
        }
    }
}