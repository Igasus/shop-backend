using System;

namespace Shop.Domain.Abstractions;

public abstract class EntityBase
{
    public Guid Id { get; set; }
}