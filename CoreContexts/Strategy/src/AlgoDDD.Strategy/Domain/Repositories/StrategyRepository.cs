using System;
using System.Threading.Tasks;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.Repositories;

public class StrategyRepository : IStrategyRepository
{
    private readonly DbContext _context;

    public StrategyRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<StrategyEntity?> GetByIdAsync(Guid id)
    {
        return await _context.Set<StrategyEntity>().FindAsync(id);
    }

    public async Task AddAsync(StrategyEntity strategy)
    {
        await _context.Set<StrategyEntity>().AddAsync(strategy);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(StrategyEntity strategy)
    {
        _context.Set<StrategyEntity>().Update(strategy);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _context.Set<StrategyEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
