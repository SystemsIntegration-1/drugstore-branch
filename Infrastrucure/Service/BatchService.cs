using AutoMapper;
using drugstore_branch.Domain.Dto;
using drugstore_branch.Domain.Model;
using drugstore_branch.Domain.Repository;
using drugstore_branch.Domain.Service;

namespace drugstore_branch.Infrastrucure.Service;

/* 
 * This class implements the IBatchService interface and provides methods 
 * to interact with batch-related operations. It uses AutoMapper to map 
 * between domain models and DTOs, and interacts with the IBatchRepository 
 * to perform CRUD operations for batches.
 */
public class BatchService : IBatchService
{
    private readonly IBatchRepository _batchRepository;
    private readonly IMapper _mapper;

    /* 
     * Constructor that initializes the BatchService with the IBatchRepository 
     * and IMapper dependencies.
     * @param batchRepository - The repository to access batch data.
     * @param mapper - The AutoMapper instance to map between models and DTOs.
     */
    public BatchService(IBatchRepository batchRepository, IMapper mapper)
    {
        _batchRepository = batchRepository;
        _mapper = mapper;
    }

    /* 
     * Retrieves all batches from the repository and maps them to BatchDto.
     * @return A collection of BatchDto representing all batches.
     */
    public async Task<IEnumerable<BatchDto>> GetAllAsync()
    {
        var batches = await _batchRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<BatchDto>>(batches);
    }

    /* 
     * Retrieves a batch by its ID from the repository and maps it to BatchDto.
     * @param id - The unique identifier of the batch.
     * @return The corresponding BatchDto, or null if the batch is not found.
     */
    public async Task<BatchDto?> GetByIdAsync(Guid id)
    {
        var batch = await _batchRepository.GetByIdAsync(id);
        return batch == null ? null : _mapper.Map<BatchDto>(batch);
    }

    /* 
     * Retrieves batches by the associated product ID from the repository 
     * and maps them to BatchDto.
     * @param productId - The unique identifier of the product.
     * @return A collection of BatchDto representing the batches for the specified product.
     */
    public async Task<IEnumerable<BatchDto>> GetByProductIdAsync(Guid productId)
    {
        var batches = await _batchRepository.GetByProductIdAsync(productId);
        return _mapper.Map<IEnumerable<BatchDto>>(batches);
    }

    /* 
     * Retrieves batches by the associated shared ID from the repository 
     * and maps them to BatchDto.
     * @param sharedId - The unique identifier of the shared entity.
     * @return A collection of BatchDto representing the batches for the specified shared entity.
     */
    public async Task<IEnumerable<BatchDto>> GetBySharedIdAsync(Guid sharedId)
    {
        var batches = await _batchRepository.GetBySharedIdAsync(sharedId);
        return _mapper.Map<IEnumerable<BatchDto>>(batches);
    }

    /* 
     * Creates a new batch based on the provided CreateBatchDto and adds it 
     * to the repository. Then, it maps the created batch to BatchDto and returns it.
     * @param batchDto - The CreateBatchDto containing the information to create the batch.
     * @return The created BatchDto.
     */
    public async Task<BatchDto> CreateAsync(CreateBatchDto batchDto)
    {
        var batch = _mapper.Map<Batch>(batchDto);
        await _batchRepository.AddAsync(batch);
        return _mapper.Map<BatchDto>(batch);
    }

    /* 
     * Deletes a batch by its ID from the repository.
     * @param id - The unique identifier of the batch to delete.
     */
    public async Task DeleteAsync(Guid id)
    {
        await _batchRepository.DeleteAsync(id);
    }

}
