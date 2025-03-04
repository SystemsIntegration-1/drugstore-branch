namespace drugstore_branch.Domain.Dto;

public class OrderDto
{
    public Guid Id { get; set; }
    public Dictionary<Guid, int> ProductQuantities { get; set; } = new Dictionary<Guid, int>();
    public int TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
}