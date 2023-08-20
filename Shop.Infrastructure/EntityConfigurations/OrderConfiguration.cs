using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.DataConventions;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.EntityConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(order => order.Index)
            .ValueGeneratedOnAdd();

        builder.OwnsOne(order => order.Price, ownedBuilder =>
        {
            ownedBuilder.Property(price => price.SubTotal)
                .HasPrecision(OrderPriceConventions.SubTotalPrecision, OrderPriceConventions.SubTotalScale);
            ownedBuilder.Property(price => price.Discount)
                .HasPrecision(OrderPriceConventions.DiscountPrecision, OrderPriceConventions.DiscountScale);
            ownedBuilder.Property(price => price.Total)
                .HasPrecision(OrderPriceConventions.TotalPrecision, OrderPriceConventions.TotalScale);
        });

        builder.HasOne(order => order.Customer)
            .WithMany(customer => customer.Orders)
            .HasForeignKey(order => order.CustomerId);
    }
}