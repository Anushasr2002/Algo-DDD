using Microsoft.EntityFrameworkCore;
using AlgoDDD.CoreContexts.Strategy.Domain.Entities;

namespace AlgoDDD.CoreContexts.Strategy.Infrastructure.Persistence
{
    public class StrategyDbContext : DbContext
    {
        public StrategyDbContext(DbContextOptions<StrategyDbContext> options)
            : base(options)
        {
        }

        // DbSets for your domain entities
        public DbSet<StrategyEntity> Strategies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Example configuration for StrategyEntity
            modelBuilder.Entity<StrategyEntity>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.Description)
                      .HasMaxLength(1000);

                // Add more property configurations as needed
            });
        }
    }
}
