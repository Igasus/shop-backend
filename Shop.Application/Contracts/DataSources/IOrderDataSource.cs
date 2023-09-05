using System.Linq;
using Shop.Domain.Entities;

namespace Shop.Application.Contracts.DataSources;

public interface IOrderDataSource
{
    IQueryable<Order> Orders { get; }
}