using Secure2D.Models;

namespace Secure2D.Repositories
{
    public interface IObject2DRepository
    {
        Task<IEnumerable<Object2D>> GetAllAsync();
        Task<Object2D?> GetByIdAsync(Guid id);
        Task<IEnumerable<Object2D>> GetByEnvironmentIdAsync(Guid environmentId);
        Task AddAsync(Object2D obj);
        Task UpdateAsync(Object2D obj);
        Task<bool> DeleteAsync(Guid id);
    }
}
