using drugstore_branch.Domain.Dto;

namespace drugstore_branch.Domain.Service;

/* 
 * Defines the contract for batch-related operations in the service layer.
 * Provides methods for retrieving, creating, and deleting batches, as well as 
 * searching by different parameters (ID, product ID, shared ID).
 */
public interface IBatchService
{
    Task<IEnumerable<BatchDto>> GetAllAsync();
    Task<BatchDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<BatchDto>> GetByProductIdAsync(Guid productId);
    Task<BatchDto> CreateAsync(CreateBatchDto batchDto);
    Task<IEnumerable<BatchDto>> GetBySharedIdAsync(Guid sharedId);
    Task DeleteAsync(Guid id);
}
