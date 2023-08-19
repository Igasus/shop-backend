using System;

namespace Shop.Application.Dto;

public abstract record EntityDtoBase
{
    public Guid Id { get; set; }
}