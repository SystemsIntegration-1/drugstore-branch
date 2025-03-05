using System;

namespace drugstore_branch.Domain.Model;

/* 
 * Represents a product in the system, including its name, description, 
 * category, price, and warehouse location.
 */
public class Product
{
    public Guid Id { get; set; }
    public Guid SharedId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }
    public int Price { get; set; }
    public required string WarehouseLocation { get; set; }
}
