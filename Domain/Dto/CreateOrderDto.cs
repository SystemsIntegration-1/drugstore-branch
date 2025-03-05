namespace drugstore_branch.Domain.Dto;

/* 
 * Data Transfer Object (DTO) used for creating a new order.
 * This DTO is used to transfer the data required to create an order in the system.
 */
public class CreateOrderDto
{
    public Dictionary<Guid, int> ProductQuantities { get; set; } = new Dictionary<Guid, int>();
    public int TotalPrice { get; set; }
}
