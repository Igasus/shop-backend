namespace Shop.Application.Dto;

public record OrderProductDtoInput
{
    public string Name { get; set; }
    public decimal UnitQuantity { get; set; }
    public string UnitMeasure { get; set; }
    public decimal PriceSubTotal { get; set; }
}