using AutoMapper;
using Shop.Domain.Entities;
using Shop.Domain.OwnedData;

namespace Shop.Application.Dto.MappingProfiles;

public class OrderProductMappingProfile : Profile
{
    public OrderProductMappingProfile()
    {
        CreateMap<OrderProductDtoInput, OrderProduct>()
            .ForMember(entity => entity.Unit, opt =>
                opt.MapFrom(dto => new Unit
                {
                    Quantity = dto.UnitQuantity,
                    Measure = dto.UnitMeasure
                }))
            .ForMember(entity => entity.Price, opt =>
                opt.MapFrom(dto => new OrderProductPrice { PerUnit = dto.PricePerUnit }));

        CreateMap<OrderProduct, OrderProductDto>()
            .ForMember(dto => dto.UnitQuantity, opt =>
                opt.MapFrom(entity => entity.Unit.Quantity))
            .ForMember(dto => dto.UnitMeasure, opt =>
                opt.MapFrom(entity => entity.Unit.Measure))
            .ForMember(dto => dto.PricePerUnit, opt =>
                opt.MapFrom(entity => entity.Price.PerUnit))
            .ForMember(dto => dto.PriceTotal, opt =>
                opt.MapFrom(entity => entity.Price.Total));
    }
}