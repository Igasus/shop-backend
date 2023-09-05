using System;
using System.Collections.Generic;
using Shop.Application.Dto.Abstractions;

namespace Shop.Application.Dto;

public record CustomerDto : EntityDtoBase
{
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    
    public IList<Guid> OrderIds { get; set; }
}