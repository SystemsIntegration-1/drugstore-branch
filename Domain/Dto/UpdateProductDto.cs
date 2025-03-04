namespace drugstore_branch.Domain.Dto;

public class UpdateProductDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int Price { get; set; }
    public required string Category { get; set; }
    public required string WarehouseLocation { get; set; }
}