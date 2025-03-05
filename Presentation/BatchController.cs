using drugstore_branch.Domain.Dto;
using drugstore_branch.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace drugstore_branch.Presentation;

/* 
 * The BatchController class is an API controller that handles HTTP requests 
 * related to batches. It provides endpoints for creating, retrieving, updating, 
 * and deleting batches. The controller uses the IBatchService to interact 
 * with batch-related business logic and provides data in the form of DTOs.
 */
[ApiController]
[Route("api/batches")]
public class BatchController : ControllerBase
{
    private readonly IBatchService _batchService;

    public BatchController(IBatchService batchService)
    {
        _batchService = batchService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBatches()
    {
        var batches = await _batchService.GetAllAsync();

        return Ok(batches);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBatchById(Guid id)
    {
        var batch = await _batchService.GetByIdAsync(id);
        return batch != null ? Ok(batch) : NotFound();
    }

    [HttpGet("product/{productId}")]
    public async Task<IActionResult> GetBatchesByProductId(Guid productId)
    {
        var batches = await _batchService.GetByProductIdAsync(productId);
        return Ok(batches);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBatch([FromBody] CreateBatchDto createBatchDto)
    {
        var batch = await _batchService.CreateAsync(createBatchDto);

        var batchDto = new CreateBatchDto
        {
            ProductId = batch.ProductId,
            Stock = batch.Stock,
            EntryDate = batch.EntryDate,
            ExpirationDate = batch.ExpirationDate,
        };

        return CreatedAtAction(nameof(GetBatchById), new { id = batch.Stock }, batchDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBatch(Guid id)
    {
        await _batchService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("shared/{sharedId}")]
    public async Task<IActionResult> GetBatchesBySharedId(Guid sharedId)
    {
        var batches = await _batchService.GetBySharedIdAsync(sharedId);
        return Ok(batches);
    }
}