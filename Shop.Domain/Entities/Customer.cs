using System.Collections.Generic;
using Shop.Domain.Abstractions;

namespace Shop.Domain.Entities;

public class Customer : EntityBase
{
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    
    public virtual ICollection<Order> Orders { get; set; }
}