using System.Collections.Generic;
using Shop.Application.Dto.Abstractions;

namespace Shop.Application.Dto.Messaging;

public record OrderDtoMessage : EntityDtoBase
{
    public int Index { get; set; }
    public decimal PriceSubTotal { get; set; }
    public decimal DiscountTotal { get; set; }
    public decimal PriceTotal { get; set; }
    
    public CustomerDtoMessage Customer { get; set; }
    public IList<OrderProductDtoMessage> Products { get; set; }
}