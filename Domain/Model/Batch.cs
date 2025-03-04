namespace drugstore_branch.Domain.Model;

public class Batch
{
    public Guid Id { get; set; } 
    public Guid ProductId { get; set; }
    public int Stock { get; set; }
    public long EntryDate { get; set; }
    public long ExpirationDate { get; set; }

    public Product? Product { get; set; }
}
