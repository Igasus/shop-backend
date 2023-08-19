namespace Shop.Domain.OwnedData;

public class OrderPrice
{
    public decimal SubTotal { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
}