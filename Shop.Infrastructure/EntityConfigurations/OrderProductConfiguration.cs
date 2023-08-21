using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;
using Shop.Infrastructure.EntityConfigurations.OwnedTablesConfigurations;

namespace Shop.Infrastructure.EntityConfigurations;

public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
{
    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        builder.OwnsOne(orderProduct => orderProduct.Unit, UnitConfiguration.Configure);
        builder.OwnsOne(orderProduct => orderProduct.Price, PriceConfiguration.Configure);
        
        builder.HasOne(orderProduct => orderProduct.Order)
            .WithMany(order => order.Products)
            .HasForeignKey(orderProduct => orderProduct.OrderId);
    }
}