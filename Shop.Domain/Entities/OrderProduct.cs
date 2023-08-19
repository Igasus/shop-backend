using System;
using Shop.Domain.Abstractions;
using Shop.Domain.OwnedData;

namespace Shop.Domain.Entities;

public class OrderProduct : EntityBase
{
    public string Name { get; set; }
    public Unit Unit { get; set; }
    public OrderProductPrice Price { get; set; }
    
    public Guid OrderId { get; set; }
    public virtual Order Order { get; set; }
}