using System;

namespace Shop.Application.Dto.Abstractions;

public abstract record EntityDtoBase
{
    public Guid Id { get; set; }
}