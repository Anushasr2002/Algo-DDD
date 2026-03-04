using Microsoft.EntityFrameworkCore;
using AlgoDDD.Strategy.Infrastructure.Models;

namespace AlgoDDD.Strategy.Infrastructure
{
    public class StrategyDbContext : DbContext
    {
        public StrategyDbContext(DbContextOptions<StrategyDbContext> options) 
            : base(options)
        {
        }

        public DbSet<StrategyDataModel> Strategies { get; set; }
        public DbSet<EvaluationResultDataModel> EvaluationResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your entity mappings here
            modelBuilder.Entity<StrategyDataModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                // Add other configurations
            });

            modelBuilder.Entity<EvaluationResultDataModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Signal).HasMaxLength(50);
                // Add other configurations
            });
        }
    }
}