using drugstore_branch.Domain.Model;

namespace drugstore_branch.Domain.Repository;

public interface IBatchRepository
{
    Task AddAsync(Batch batch);
    Task<List<Batch>> GetAllAsync();
    Task<Batch> GetByIdAsync(Guid id);
    Task<List<Batch>> GetByProductIdAsync(Guid productId);
    Task UpdateAsync(Batch batch);
    Task DeleteAsync(Guid id);
}