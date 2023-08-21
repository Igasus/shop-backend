using System;
using System.Collections.Generic;
using Shop.Domain.Abstractions;
using Shop.Domain.Entities.Owned;

namespace Shop.Domain.Entities;

public class Order : EntityBase
{
    public int Index { get; set; }
    public Price Price { get; set; } = new();
    public Discount RequestedDiscount { get; set; } = new();
    public Discount ResultDiscount { get; set; } = new();
    
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    
    public virtual ICollection<OrderProduct> Products { get; set; }
}