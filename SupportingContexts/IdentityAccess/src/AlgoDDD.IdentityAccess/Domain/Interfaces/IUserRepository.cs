using System.Threading.Tasks;
using AlgoDDD.IdentityAccess.Domain.Models;

namespace AlgoDDD.IdentityAccess.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsync(string id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(string id);
    }
}
