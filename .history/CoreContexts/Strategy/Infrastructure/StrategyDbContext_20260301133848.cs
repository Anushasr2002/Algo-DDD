using Microsoft.EntityFrameworkCore;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.ValueObjects;
using System;
using System.Linq;

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

            modelBuilder.Entity<StrategyEntity>(entity =>
            {
                entity.HasKey(e => e.Id);

                // ✅ Configure StrategyId value object conversion
                entity.Property(e => e.Id)
                      .HasConversion(
                          id => id.Value,                // to Guid
                          value => new StrategyId(value) // back to StrategyId
                      );

                // ✅ Map StrategyType (value object)
                entity.Property(e => e.Type)
                      .HasConversion(
                          type => type.Value,                // to string/int
                          value => new StrategyType(value)   // back to StrategyType
                      )
                      .IsRequired();

                // ✅ Map Symbols list as comma‑separated string
                entity.Property(e => e.Symbols)
                      .HasConversion(
                          v => string.Join(",", v),
                          v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                      );
            });
        }
    }
}
