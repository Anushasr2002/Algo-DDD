using System.Threading;
using System.Threading.Tasks;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.Interfaces;
using AlgoDDD.Strategy.Domain.ValueObjects;
using AlgoDDD.Strategy.Infrastructure.Mappers;
using AlgoDDD.Strategy.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

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
            // Get the data model from database
            var dataModel = await _dbContext.Strategies
                .FirstOrDefaultAsync(s => s.Id == id.Value, cancellationToken); // Assuming Id is int/long in data model
            
            // Map to domain entity
            return dataModel != null ? StrategyMappers.ToDomain(dataModel) : null;
        }

        public async Task<StrategyEntity> AddAsync(StrategyEntity entity, CancellationToken cancellationToken = default)
        {
            // Map domain entity to data model
            var dataModel = StrategyMappers.ToInfrastructure(entity);
            
            // Add to database
            _dbContext.Strategies.Add(dataModel);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return entity;
        }

        public async Task UpdateAsync(StrategyEntity entity, CancellationToken cancellationToken = default)
        {
            // Get existing data model
            var existingDataModel = await _dbContext.Strategies
                .FirstOrDefaultAsync(s => s.Id == entity.Id.Value, cancellationToken);
                
            if (existingDataModel != null)
            {
                // Update properties
                existingDataModel.Name = entity.Name;
                existingDataModel.Description = entity.Description;
                existingDataModel.IsActive = entity.IsActive;
                existingDataModel.UpdatedAt = DateTime.UtcNow;
                // Update other properties as needed
                
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task SaveEvaluationResultAsync(EvaluationResult result, CancellationToken cancellationToken = default)
        {
            // You need to create an EvaluationResultDataModel and EvaluationResultMapper
            var dataModel = new EvaluationResultDataModel
            {
                Id = result.Id,
                StrategyId = result.StrategyId.Value,
                Timestamp = result.Timestamp,
                Signal = result.Signal,
                Confidence = result.Confidence
                // Add other properties
            };
            
            _dbContext.EvaluationResults.Add(dataModel);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}