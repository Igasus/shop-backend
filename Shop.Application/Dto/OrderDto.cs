using System;
using System.Collections.Generic;

namespace Shop.Application.Dto;

public record OrderDto : EntityDtoBase
{
    public int Index { get; set; }
    
    public decimal PriceSubTotal { get; set; }
    public decimal PriceDiscount { get; set; }
    public decimal PriceTotal { get; set; }
    
    public Guid CustomerId { get; set; }
    public IList<OrderProductDto> Products { get; set; }
}