namespace drugstore_branch.Domain.Dto;

/* 
 * Data Transfer Object (DTO) for updating product information.
 * This DTO is used to modify an existing product's details within the system.
 */

public class UpdateProductDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int Price { get; set; }
    public required string Category { get; set; }
    public required string WarehouseLocation { get; set; }
}
