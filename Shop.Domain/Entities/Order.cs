using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Domain.Abstractions;
using Shop.Domain.Entities.Owned;
using Shop.Domain.Enums;

namespace Shop.Domain.Entities;

public class Order : EntityBase
{
    public int Index { get; set; }
    public Price Price { get; set; } = new();
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public Discount RequestedDiscount { get; set; } = new();
    public Discount ResultDiscount { get; set; } = new();
    
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    
    public virtual ICollection<OrderProduct> Products { get; set; }
    
    public void ActualizeCalculatedData()
    {
        foreach (var orderProduct in Products)
        {
            orderProduct.ActualizeTotalPrice();
        }
        
        ActualizeSubTotalPrice();
        ActualizeResultDiscount();
        ActualizeTotalPrice();
    }

    private void ActualizeSubTotalPrice()
    {
        Price.SubTotal = Products.Sum(product => product.Price.Total);
    }

    private void ActualizeResultDiscount()
    {
        ResultDiscount.Value = Math.Min(
            Price.SubTotal,
            Price.SubTotal * (RequestedDiscount.Percent / 100) + RequestedDiscount.Value);
        
        ResultDiscount.Percent = Price.SubTotal == 0
            ? 100
            : 100 * ResultDiscount.Value / Price.SubTotal;
    }

    private void ActualizeTotalPrice()
    {
        Price.Total = Price.SubTotal - ResultDiscount.Value;
    }
}