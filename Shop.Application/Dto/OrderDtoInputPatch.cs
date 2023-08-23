using Shop.Domain.Enums;

namespace Shop.Application.Dto;

public class OrderDtoInputPatch
{
    public OrderStatus Status { get; set; }
}