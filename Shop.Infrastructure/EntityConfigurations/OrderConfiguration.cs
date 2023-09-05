using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;
using Shop.Infrastructure.EntityConfigurations.OwnedTablesConfigurations;

namespace Shop.Infrastructure.EntityConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(order => order.Index)
            .ValueGeneratedOnAdd();

        builder.OwnsOne(order => order.Price, PriceConfiguration.Configure);
        builder.OwnsOne(order => order.RequestedDiscount, DiscountConfiguration.Configure);
        builder.OwnsOne(order => order.ResultDiscount, DiscountConfiguration.Configure);

        builder.HasOne(order => order.Customer)
            .WithMany(customer => customer.Orders)
            .HasForeignKey(order => order.CustomerId);
    }
}