using AutoMapper;
using drugstore_branch.Domain.Dto;
using drugstore_branch.Domain.Model;
using drugstore_branch.Domain.Repository;
using drugstore_branch.Domain.Service;

namespace drugstore_branch.Infrastrucure.Service;

public class BatchService : IBatchService
{
    private readonly IBatchRepository _batchRepository;
    private readonly IMapper _mapper;

    public BatchService(IBatchRepository batchRepository, IMapper mapper)
    {
        _batchRepository = batchRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BatchDto>> GetAllAsync()
    {
        var batches = await _batchRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<BatchDto>>(batches);
    }

    public async Task<BatchDto?> GetByIdAsync(Guid id)
    {
        var batch = await _batchRepository.GetByIdAsync(id);
        return batch == null ? null : _mapper.Map<BatchDto>(batch);
    }

    public async Task<IEnumerable<BatchDto>> GetByProductIdAsync(Guid productId)
    {
        var batches = await _batchRepository.GetByProductIdAsync(productId);
        return _mapper.Map<IEnumerable<BatchDto>>(batches);
    }

    public async Task<BatchDto> CreateAsync(CreateBatchDto batchDto)
    {
        var batch = _mapper.Map<Batch>(batchDto);
        await _batchRepository.AddAsync(batch);
        return _mapper.Map<BatchDto>(batch);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _batchRepository.DeleteAsync(id);
    }

}