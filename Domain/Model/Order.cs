using System;

namespace drugstore_branch.Domain.Model;

/* 
 * Represents an order in the system, containing product quantities, 
 * total price, and the date the order was placed.
 */
public class Order : IEntity
{
    public Guid Id { get; set; }
    public Dictionary<Guid, int> ProductQuantities { get; set; } = new Dictionary<Guid, int>();
    public int TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
}
