using Microsoft.EntityFrameworkCore;
using AlgoDDD.Strategy.Domain.Entities;
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
            modelBuilder.Entity<StrategyDataModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Type).IsRequired().HasMaxLength(50);
                entity.Property(e => e.TimeFrame).HasMaxLength(10);
                entity.Property(e => e.Parameters).HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
                entity.Property(e => e.Symbols).HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
            });

            modelBuilder.Entity<EvaluationResultDataModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Signal).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.StrategyId);
            });
        }
    }
}