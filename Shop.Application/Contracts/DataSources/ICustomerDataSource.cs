using System.Linq;
using Shop.Domain.Entities;

namespace Shop.Application.Contracts.DataSources;

public interface ICustomerDataSource
{
    IQueryable<Customer> Customers { get; }
}