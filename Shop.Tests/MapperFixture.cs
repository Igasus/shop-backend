using System;
using AutoMapper;
using Shop.Application.Dto.MappingProfiles;

namespace Shop.Tests;

public class MapperFixture : IDisposable
{
    public IMapper Mapper { get; }

    public MapperFixture()
    {
        var mapperConfiguration = new MapperConfiguration(config =>
        {
            config.AddProfile<CustomerMappingProfile>();
            config.AddProfile<OrderMappingProfile>();
            config.AddProfile<OrderProductMappingProfile>();
        });

        Mapper = mapperConfiguration.CreateMapper();
    }
    
    public void Dispose()
    {
    }
}