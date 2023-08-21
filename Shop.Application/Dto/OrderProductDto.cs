using Shop.Application.Dto.Abstractions;

namespace Shop.Application.Dto;

public record OrderProductDto : EntityDtoBase
{
    public string Name { get; set; }
    public decimal UnitQuantity { get; set; }
    public string UnitMeasure { get; set; }
    public decimal PriceSubTotal { get; set; }
    public decimal PriceTotal { get; set; }
}