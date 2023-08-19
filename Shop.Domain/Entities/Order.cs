using System;
using System.Collections.Generic;
using Shop.Domain.Abstractions;
using Shop.Domain.OwnedData;

namespace Shop.Domain.Entities;

public class Order : EntityBase
{
    public int Index { get; set; }
    public OrderPrice Price { get; set; }
    
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    
    public virtual ICollection<OrderProduct> Products { get; set; }
}