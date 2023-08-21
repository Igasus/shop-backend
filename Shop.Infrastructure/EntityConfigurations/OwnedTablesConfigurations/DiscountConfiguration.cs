using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.DataConventions;
using Shop.Domain.Entities.Owned;

namespace Shop.Infrastructure.EntityConfigurations.OwnedTablesConfigurations;

public class DiscountConfiguration
{
    public static void Configure<TOwnerEntity>(OwnedNavigationBuilder<TOwnerEntity, Discount> ownedBuilder)
        where TOwnerEntity : class
    {
        ownedBuilder.Property(discount => discount.Percent)
            .HasPrecision(DiscountConventions.PercentPrecision, DiscountConventions.PercentScale);
        
        ownedBuilder.Property(discount => discount.Value)
            .HasPrecision(DiscountConventions.ValuePrecision, DiscountConventions.ValueScale);
        
        ownedBuilder.Property(discount => discount.Total)
            .HasPrecision(DiscountConventions.TotalPrecision, DiscountConventions.TotalScale);
    }
}