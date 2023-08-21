using System;
using Shop.Domain.Abstractions;
using Shop.Domain.Entities.Owned;

namespace Shop.Domain.Entities;

public class OrderProduct : EntityBase
{
    public string Name { get; set; }
    public Unit Unit { get; set; } = new();
    public Price Price { get; set; } = new();
    
    public Guid OrderId { get; set; }
    public virtual Order Order { get; set; }
}