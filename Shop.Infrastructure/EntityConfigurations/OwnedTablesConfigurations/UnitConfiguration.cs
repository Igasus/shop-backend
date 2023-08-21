using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.DataConventions;
using Shop.Domain.Entities.Owned;

namespace Shop.Infrastructure.EntityConfigurations.OwnedTablesConfigurations;

public static class UnitConfiguration
{
    public static void Configure<TOwnerEntity>(OwnedNavigationBuilder<TOwnerEntity, Unit> ownedBuilder)
        where TOwnerEntity : class
    {
        ownedBuilder.Property(unit => unit.Quantity)
            .HasPrecision(UnitConventions.QuantityPrecision, UnitConventions.QuantityScale);
    }
}