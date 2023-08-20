using AutoMapper;
using Shop.Domain.Entities;

namespace Shop.Application.Dto.MappingProfiles;

public class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<CustomerDtoInput, Customer>();
        CreateMap<Customer, CustomerDto>();
    }
}