namespace drugstore_branch.Domain.Dto;

/* 
 * Data Transfer Object (DTO) used for creating a new batch of products.
 * This DTO is used to transfer data when a new batch is being added to the system.
 */
public class CreateBatchDto
{
    public Guid ProductId { get; set; }
    public int Stock { get; set; }
    public long EntryDate { get; set; }
    public long ExpirationDate { get; set; }
}
