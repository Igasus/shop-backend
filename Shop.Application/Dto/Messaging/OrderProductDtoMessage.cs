namespace Shop.Application.Dto.Messaging;

public record OrderProductDtoMessage
{
    public string Name { get; set; }
    public decimal UnitQuantity { get; set; }
    public string UnitMeasure { get; set; }
};