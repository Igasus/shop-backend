namespace Shop.Application.Dto;

public record CustomerDtoInput
{
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}