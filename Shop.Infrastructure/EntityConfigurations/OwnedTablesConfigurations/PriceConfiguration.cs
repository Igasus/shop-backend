using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.DataConventions;
using Shop.Domain.Entities.Owned;

namespace Shop.Infrastructure.EntityConfigurations.OwnedTablesConfigurations;

public static class PriceConfiguration
{
    public static void Configure<TOwnerEntity>(OwnedNavigationBuilder<TOwnerEntity, Price> ownedBuilder)
        where TOwnerEntity : class
    {
        ownedBuilder.Property(price => price.SubTotal)
            .HasPrecision(PriceConventions.SubTotalPrecision, PriceConventions.SubTotalScale);

        ownedBuilder.Property(price => price.Total)
            .HasPrecision(PriceConventions.TotalPrecision, PriceConventions.TotalScale);
    }
}