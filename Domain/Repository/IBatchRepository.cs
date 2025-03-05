using drugstore_branch.Domain.Model;

namespace drugstore_branch.Domain.Repository;

/* 
 * Defines the contract for batch data operations, including creation, 
 * retrieval, update, and deletion of batch records.
 */
public interface IBatchRepository
{
    Task AddAsync(Batch batch);
    Task<List<Batch>> GetAllAsync();
    Task<Batch> GetByIdAsync(Guid id);
    Task<List<Batch>> GetByProductIdAsync(Guid productId);
    Task<List<Batch>> GetBySharedIdAsync(Guid sharedId);
    Task UpdateAsync(Batch batch);
    Task DeleteAsync(Guid id);
}
