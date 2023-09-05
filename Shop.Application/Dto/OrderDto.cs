using System;
using System.Collections.Generic;
using Shop.Application.Dto.Abstractions;
using Shop.Domain.Enums;

namespace Shop.Application.Dto;

public record OrderDto : EntityDtoBase
{
    public int Index { get; set; }
    public OrderStatus Status { get; set; }
    public decimal PriceSubTotal { get; set; }
    public decimal PriceTotal { get; set; }
    public decimal RequestedDiscountPercent { get; set; }
    public decimal RequestedDiscountValue { get; set; }
    public decimal ResultDiscountPercent { get; set; }
    public decimal ResultDiscountValue { get; set; }

    public Guid CustomerId { get; set; }
    public IList<OrderProductDto> Products { get; set; }
}