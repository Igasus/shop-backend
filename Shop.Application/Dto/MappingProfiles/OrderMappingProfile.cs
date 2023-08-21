using AutoMapper;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Owned;

namespace Shop.Application.Dto.MappingProfiles;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<OrderDtoInput, Order>()
            .ForMember(entity => entity.RequestedDiscount, opt =>
                opt.MapFrom(dto => new Discount
                {
                    Percent = dto.RequestedDiscountPercent,
                    Value = dto.RequestedDiscountValue
                }));

        CreateMap<Order, OrderDto>()
            .ForMember(dto => dto.PriceSubTotal, opt =>
                opt.MapFrom(entity => entity.Price.SubTotal))
            .ForMember(dto => dto.PriceTotal, opt =>
                opt.MapFrom(entity => entity.Price.Total))
            .ForMember(dto => dto.RequestedDiscountPercent, opt =>
                opt.MapFrom(entity => entity.RequestedDiscount.Percent))
            .ForMember(dto => dto.RequestedDiscountValue, opt =>
                opt.MapFrom(entity => entity.RequestedDiscount.Value))
            .ForMember(dto => dto.ResultDiscountValue, opt =>
                opt.MapFrom(entity => entity.ResultDiscount.Percent))
            .ForMember(dto => dto.ResultDiscountValue, opt =>
                opt.MapFrom(entity => entity.ResultDiscount.Value));
    }
}