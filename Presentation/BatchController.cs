using drugstore_branch.Domain.Dto;
using drugstore_branch.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace drugstore_branch.Presentation;

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

        var batchDto = new BatchDto
        {
            Stock = batch.Stock,
            EntryDate = batch.EntryDate,
            ExpirationDate = batch.ExpirationDate
        };

        return CreatedAtAction(nameof(GetBatchById), new { id = batch.Stock }, batchDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBatch(Guid id)
    {
        await _batchService.DeleteAsync(id);
        return NoContent();
    }
}