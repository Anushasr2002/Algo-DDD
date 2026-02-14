using System.Collections.Concurrent;
using AlgoDDD.IdentityAccess.Domain.Aggregates;
using AlgoDDD.IdentityAccess.Domain.Repositories;
using AlgoDDD.IdentityAccess.Domain.ValueObjects;

namespace AlgoDDD.IdentityAccess.Infrastructure.Persistence;

public class InMemoryUserRepository : IUserRepository
{
    private readonly ConcurrentDictionary<Guid, User> _users = new();
    private readonly ConcurrentDictionary<string, Guid> _emailIndex = new();
    
    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _users.TryGetValue(id, out var user);
        return Task.FromResult(user);
    }
    
    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        if (_emailIndex.TryGetValue(email.ToLowerInvariant(), out var userId))
        {
            _users.TryGetValue(userId, out var user);
            return Task.FromResult(user);
        }
        return Task.FromResult<User?>(null);
    }
    
    public Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_users.Values.AsEnumerable());
    }
    
    public Task<IEnumerable<User>> GetByRoleAsync(UserRole role, CancellationToken cancellationToken = default)
    {
        var users = _users.Values.Where(u => u.Role == role);
        return Task.FromResult(users);
    }
    
    public Task<bool> ExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_emailIndex.ContainsKey(email.ToLowerInvariant()));
    }
    
    public Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        _users.TryAdd(user.Id, user);
        _emailIndex.TryAdd(user.Email.ToString().ToLowerInvariant(), user.Id);
        return Task.CompletedTask;
    }
    
    public Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        _users.TryUpdate(user.Id, user, user);
        return Task.CompletedTask;
    }
    
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (_users.TryRemove(id, out var user))
        {
            _emailIndex.TryRemove(user.Email.ToString().ToLowerInvariant(), out _);
        }
        return Task.CompletedTask;
    }
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // In-memory doesn't need to save changes
        return Task.FromResult(1);
    }
}
