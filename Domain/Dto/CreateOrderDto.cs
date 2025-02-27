namespace drugstore_branch.Domain.Dto;

public class CreateOrderDto
{
    public List<Guid> ProductIds { get; set; } = new List<Guid>();
    public double TotalPrice { get; set; }
}