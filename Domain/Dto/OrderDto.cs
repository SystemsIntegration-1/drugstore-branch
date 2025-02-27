namespace drugstore_branch.Domain.Dto;

public class OrderDto
{
    public List<Guid> ProductIds { get; set; } = [];
    public double TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
}