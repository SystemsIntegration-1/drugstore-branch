namespace drugstore_branch.Domain.Dto;

public class OrderDto
{
    public Guid Id { get; set; }
    public List<Guid> ProductIds { get; set; } = new List<Guid>();
    public double TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
}