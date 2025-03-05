namespace drugstore_branch.Domain.Dto;

/* 
 * Data Transfer Object (DTO) representing a batch of products.
 * This DTO is used to transfer data related to a product batch.
 */
public class BatchDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Stock { get; set; }
    public long EntryDate { get; set; }
    public long ExpirationDate { get; set; }
}
