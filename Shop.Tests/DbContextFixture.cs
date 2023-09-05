using System;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;

namespace Shop.Tests;

public class DbContextFixture : IDisposable
{
    public ShopDbContext ShopDbContext { get; }

    public DbContextFixture()
    {
        var dbContextOptions = new DbContextOptionsBuilder<ShopDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        ShopDbContext = new ShopDbContext(dbContextOptions);
    }
    
    public void Dispose()
    {
        ShopDbContext.Database.EnsureDeleted();
        
        ShopDbContext.Dispose();
    }
}