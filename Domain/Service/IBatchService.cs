using drugstore_branch.Domain.Dto;

namespace drugstore_branch.Domain.Service;

public interface IBatchService
{
    Task<IEnumerable<BatchDto>> GetAllAsync();
    Task<BatchDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<BatchDto>> GetByProductIdAsync(Guid productId);
    Task<BatchDto> CreateAsync(CreateBatchDto batchDto);
    Task<IEnumerable<BatchDto>> GetBySharedIdAsync(Guid sharedId);
    Task DeleteAsync(Guid id);
}