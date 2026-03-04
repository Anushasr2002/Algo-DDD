// File: C:\Algo-DDD\CoreContexts\Strategy\src\AlgoDDD.Strategy\Infrastructure\Models\StrategyDataModel.cs
namespace AlgoDDD.Strategy.Infrastructure.Models
{
    public class StrategyDataModel
    {
        public Guid Id { get; set; }  // Changed from int to Guid
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public List<string> Parameters { get; set; } = new();
        
        // Add these if they exist in your database
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}