using System.Linq;
using AutoMapper;
using Shop.Domain.Entities;

namespace Shop.Application.Dto.MappingProfiles;

public class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<CustomerDtoInput, Customer>();

        CreateMap<Customer, CustomerDto>()
            .ForMember(dto => dto.OrderIds, opt =>
                opt.MapFrom(entity => entity.Orders.Select(order => order.Id)));
    }
}