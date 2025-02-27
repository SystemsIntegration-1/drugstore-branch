using System;

namespace drugstore_branch.Domain.Model;

public class Order : IEntity
{
    public Guid Id { get; set; }
    public Dictionary<Guid, int> ProductQuantities { get; set; } = new Dictionary<Guid, int>();
    public double TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
}
