namespace drugstore_branch.Domain.Dto;

/* 
 * Data Transfer Object (DTO) for creating a new product.
 * This DTO is used to transfer product-related data when adding a new product to the system.
 */
public class CreateProductDto
{
    public required Guid SharedId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int Price { get; set; }
    public required string Category { get; set; }
    public required string WarehouseLocation { get; set; }
}
