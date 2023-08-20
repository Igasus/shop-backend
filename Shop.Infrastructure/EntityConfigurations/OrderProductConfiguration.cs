using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.DataConventions;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.EntityConfigurations;

public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
{
    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        builder.OwnsOne(orderProduct => orderProduct.Unit, ownedBuilder =>
            ownedBuilder.Property(unit => unit.Quantity)
                .HasPrecision(UnitConventions.QuantityPrecision, UnitConventions.QuantityScale));
        
        builder.OwnsOne(orderProduct => orderProduct.Price, ownedBuilder =>
        {
            ownedBuilder.Property(price => price.PerUnit)
                .HasPrecision(OrderProductPriceConventions.PerUnitPrecision, OrderProductPriceConventions.PerUnitScale);
            ownedBuilder.Property(price => price.Total)
                .HasPrecision(OrderProductPriceConventions.TotalPrecision, OrderProductPriceConventions.TotalScale);
        });
        
        builder.HasOne(orderProduct => orderProduct.Order)
            .WithMany(order => order.Products)
            .HasForeignKey(orderProduct => orderProduct.OrderId);
    }
}