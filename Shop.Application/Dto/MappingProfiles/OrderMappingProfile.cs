using AutoMapper;
using Shop.Domain.Entities;

namespace Shop.Application.Dto.MappingProfiles;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<OrderDtoInput, Order>();

        CreateMap<Order, OrderDto>()
            .ForMember(dto => dto.PriceSubTotal, opt =>
                opt.MapFrom(entity => entity.Price.SubTotal))
            .ForMember(dto => dto.PriceDiscount, opt =>
                opt.MapFrom(entity => entity.Price.Discount))
            .ForMember(dto => dto.PriceTotal, opt =>
                opt.MapFrom(entity => entity.Price.Total));
    }
}