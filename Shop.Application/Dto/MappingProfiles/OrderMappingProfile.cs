using AutoMapper;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Owned;

namespace Shop.Application.Dto.MappingProfiles;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<OrderDtoInput, Order>()
            .ForMember(entity => entity.Discount, opt =>
                opt.MapFrom(dto => new Discount
                {
                    Percent = dto.DiscountPercent,
                    Value = dto.DiscountValue
                }));

        CreateMap<Order, OrderDto>()
            .ForMember(dto => dto.PriceSubTotal, opt =>
                opt.MapFrom(entity => entity.Price.SubTotal))
            .ForMember(dto => dto.PriceTotal, opt =>
                opt.MapFrom(entity => entity.Price.Total))
            .ForMember(dto => dto.DiscountPercent, opt =>
                opt.MapFrom(entity => entity.Discount.Percent))
            .ForMember(dto => dto.DiscountValue, opt =>
                opt.MapFrom(entity => entity.Discount.Value))
            .ForMember(dto => dto.DiscountTotal, opt =>
                opt.MapFrom(entity => entity.Discount.Total));
    }
}