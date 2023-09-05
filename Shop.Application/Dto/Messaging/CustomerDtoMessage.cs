using Shop.Application.Dto.Abstractions;

namespace Shop.Application.Dto.Messaging;

public record CustomerDtoMessage : EntityDtoBase
{
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}