using System;

namespace Shop.Tests.DataGenerators;

public static class TestValueGenerator
{
    public static decimal GetRandomDecimal(decimal from = 0, decimal? to = null)
    {
        var result = (decimal)Random.Shared.NextDouble();

        if (to.HasValue)
        {
            result *= to.Value - from;
        }

        result += from;

        return result;
    }
}