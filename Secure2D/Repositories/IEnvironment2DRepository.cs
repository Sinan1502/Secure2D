using Secure2D.Models;

namespace Secure2D.Repositories
{
    public interface IEnvironment2DRepository
    {
        Task<IEnumerable<Environment2D>> GetAllAsync();
        Task<Environment2D?> GetByIdAsync(Guid id);
        Task AddAsync(Environment2D environment);
        Task UpdateAsync(Environment2D environment);
        Task<bool> DeleteAsync(Guid id);
    }
}
