namespace drugstore_branch.Domain.Dto;

public class BatchDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Stock { get; set; }
    public long EntryDate { get; set; }
    public long ExpirationDate { get; set; }
}