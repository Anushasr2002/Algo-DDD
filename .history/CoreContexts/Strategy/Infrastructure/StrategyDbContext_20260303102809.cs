using Microsoft.EntityFrameworkCore;
using AlgoDDD.Strategy.Infrastructure.Entities;

namespace AlgoDDD.Strategy.Infrastructure
{
    public class StrategyDbContext : DbContext
    {
        public StrategyDbContext(DbContextOptions<StrategyDbContext> options)
            : base(options)
        {
        }

        public DbSet<StrategyEntity> Strategies { get; set; }
        public DbSet<EvaluationResultEntity> EvaluationResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure EvaluationResultEntity
            modelBuilder.Entity<EvaluationResultEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Symbol).IsRequired();
                entity.Property(e => e.Action).IsRequired();
                entity.Property(e => e.Price).HasColumnType("decimal(18,4)");
                entity.Property(e => e.Notes).HasMaxLength(500);
            });
        }
    }
}
