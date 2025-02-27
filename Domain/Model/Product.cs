using System;

namespace drugstore_branch.Domain.Model;

public class Product : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
}
