using System;
using System.Threading;
using System.Threading.Tasks;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.Interfaces;
using AlgoDDD.Strategy.Domain.ValueObjects;
using AlgoDDD.Strategy.Infrastructure.Mappers;
using AlgoDDD.Strategy.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AlgoDDD.Strategy.Infrastructure.Repository
{
    public class StrategyRepository : IStrategyRepository
    {
        private readonly StrategyDbContext _dbContext;

        public StrategyRepository(StrategyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StrategyEntity?> GetByIdAsync(StrategyId id, CancellationToken cancellationToken = default)
        {
            var dataModel = await _dbContext.Strategies
                .FirstOrDefaultAsync(s => s.Id == id.Value, cancellationToken);
            
            return dataModel != null ? StrategyMappers.ToDomain(dataModel) : null;
        }

        public async Task<StrategyEntity> AddAsync(StrategyEntity entity, CancellationToken cancellationToken = default)
        {
            var dataModel = StrategyMappers.ToInfrastructure(entity);
            _dbContext.Strategies.Add(dataModel);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task UpdateAsync(StrategyEntity entity, CancellationToken cancellationToken = default)
        {
            var existingDataModel = await _dbContext.Strategies
                .FirstOrDefaultAsync(s => s.Id == entity.Id.Value, cancellationToken);
                
            if (existingDataModel != null)
            {
                // Update using the entity's public methods instead of setting properties directly
                existingDataModel.Name = entity.Name;
                existingDataModel.Description = entity.Description;
                existingDataModel.Type = entity.Type.ToString();
                existingDataModel.Parameters = entity.Parameters;
                existingDataModel.Symbols = entity.Symbols;
                existingDataModel.TimeFrame = entity.TimeFrame;
                existingDataModel.MaxPositionSize = entity.MaxPositionSize;
                existingDataModel.StopLoss = entity.StopLoss;
                existingDataModel.TakeProfit = entity.TakeProfit;
                existingDataModel.UpdatedAt = DateTime.UtcNow;
                
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task SaveEvaluationResultAsync(EvaluationResult result, CancellationToken cancellationToken = default)
        {
            var dataModel = new EvaluationResultDataModel
            {
                Id = result.Id,
                StrategyId = result.StrategyId.Value,
                Timestamp = result.Timestamp,
                Signal = result.Signal,
                Confidence = result.Confidence
            };
            
            await _dbContext.EvaluationResults.AddAsync(dataModel, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}