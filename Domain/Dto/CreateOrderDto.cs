namespace drugstore_branch.Domain.Dto;

public class CreateOrderDto
{
    public Dictionary<Guid, int> ProductQuantities { get; set; } = new Dictionary<Guid, int>();
    public int TotalPrice { get; set; }
}