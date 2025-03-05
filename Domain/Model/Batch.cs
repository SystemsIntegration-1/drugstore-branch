namespace drugstore_branch.Domain.Model;

/* 
 * Represents a batch of products in the system. 
 * A batch is associated with a specific product and contains stock, entry, and expiration details.
 */
public class Batch
{
    public Guid Id { get; set; } 
    public Guid ProductId { get; set; }
    public int Stock { get; set; }
    public long EntryDate { get; set; }
    public long ExpirationDate { get; set; }
    public Product? Product { get; set; }
}
