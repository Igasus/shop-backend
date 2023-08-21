using AutoMapper;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Owned;

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
                opt.MapFrom(dto => new Price { SubTotal = dto.PriceSubTotal }));

        CreateMap<OrderProduct, OrderProductDto>()
            .ForMember(dto => dto.UnitQuantity, opt =>
                opt.MapFrom(entity => entity.Unit.Quantity))
            .ForMember(dto => dto.UnitMeasure, opt =>
                opt.MapFrom(entity => entity.Unit.Measure))
            .ForMember(dto => dto.PriceSubTotal, opt =>
                opt.MapFrom(entity => entity.Price.SubTotal))
            .ForMember(dto => dto.PriceTotal, opt =>
                opt.MapFrom(entity => entity.Price.Total));
    }
}