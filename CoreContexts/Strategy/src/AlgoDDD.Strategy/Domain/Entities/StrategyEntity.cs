using AlgoDDD.SharedKernel.Domain.BaseClasses;

namespace AlgoDDD.Strategy.Domain.Entities
{
    /// <summary>
    /// Represents a trading strategy definition in the domain.
    /// </summary>
    public class StrategyEntity : Entity
    {
        /// <summary>
        /// Unique name of the strategy (e.g., "RSI Breakout").
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Description of the strategy logic and purpose.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the strategy is currently active.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Optional category or tag for grouping strategies.
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// Date/time when the strategy was created.
        /// </summary>
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        /// <summary>
        /// Date/time when the strategy was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; private set; }

        /// <summary>
        /// Updates the strategy details.
        /// </summary>
        public void Update(string name, string description, bool isActive, string? category = null)
        {
            Name = name;
            Description = description;
            IsActive = isActive;
            Category = category;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
