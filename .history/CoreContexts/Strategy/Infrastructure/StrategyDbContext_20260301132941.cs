using Microsoft.EntityFrameworkCore;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Infrastructure
{
    public class StrategyDbContext : DbContext
    {
        public StrategyDbContext(DbContextOptions<StrategyDbContext> options)
            : base(options)
        {
        }

        public DbSet<StrategyEntity> Strategies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Example: configure StrategyEntity
            modelBuilder.Entity<StrategyEntity>(entity =>
            {
                entity.HasKey(e => e.Id);

            // Remove mappings for Name, Description, TimeFrame, etc.
            
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Description);
                entity.Property(e => e.Type).IsRequired();
                entity.Property(e => e.TimeFrame).IsRequired();
                entity.Property(e => e.MaxPositionSize);
                entity.Property(e => e.StopLoss);
                entity.Property(e => e.TakeProfit);

                // If StrategyId is a value object, configure conversion
                entity.Property(e => e.Id)
                      .HasConversion(
                          id => id.Value,          // to Guid
                          value => new StrategyId(value) // back to StrategyId
                      );
            });
        }
    }
}
