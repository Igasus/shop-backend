using System;
using System.Collections.Generic;

namespace Shop.Application.Dto;

public record OrderDtoInput
{
    public Guid CustomerId { get; set; }
    public decimal RequestedDiscountPercent { get; set; }
    public decimal RequestedDiscountValue { get; set; }
    
    public IList<OrderProductDtoInput> Products { get; set; }
}