using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Infrastructure.Models; // Assuming you have this

namespace AlgoDDD.Strategy.Infrastructure.Mappers
{
    public static class StrategyMappers
    {
        // Add your mapping methods here
        // Example:
        public static StrategyEntity ToDomain(StrategyDataModel model)
        {
            // Your mapping logic
            return new StrategyEntity();
        }
        
        public static StrategyDataModel ToInfrastructure(StrategyEntity entity)
        {
            // Your mapping logic
            return new StrategyDataModel();
        }
    }
}