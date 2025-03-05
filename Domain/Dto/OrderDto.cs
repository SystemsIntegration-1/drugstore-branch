namespace drugstore_branch.Domain.Dto;

/* 
 * Data Transfer Object (DTO) for representing an order.
 * This DTO is used to transfer order-related data within the system.
 */
public class OrderDto
{
    public Guid Id { get; set; }
    public Dictionary<Guid, int> ProductQuantities { get; set; } = new Dictionary<Guid, int>();
    public int TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
}
