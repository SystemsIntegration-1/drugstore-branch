using System;

namespace drugstore_branch.Domain.Model;

public class Order : IEntity
{
    public Guid Id {set; get;}
    public required List<Product> Products {get; set;}
    public double TotalPrice {get; set;}
    public DateTime OrderDate {get; set;}
}
