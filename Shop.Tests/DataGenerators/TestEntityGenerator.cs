using System;
using System.Collections.Generic;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Owned;

namespace Shop.Tests.DataGenerators;

public static class TestEntityGenerator
{
    public static Customer GenerateCustomer(string uniqueValue, IList<Order> orders = null)
    {
        orders ??= new List<Order>();
        
        return new Customer
        {
            FullName = $"Customer-{uniqueValue}",
            PhoneNumber = $"Phone-{uniqueValue}",
            Email = $"customer.{uniqueValue}@mail.com",
            Orders = orders
        };
    }

    public static Order GenerateOrder(
        Discount requestedDiscount = null,
        Guid customerId = default,
        IList<OrderProduct> products = null)
    {
        requestedDiscount ??= new Discount();
        products ??= new List<OrderProduct>();

        var order = new Order
        {
            RequestedDiscount = requestedDiscount,
            CustomerId = customerId,
            Products = products
        };
        
        order.ActualizeCalculatedData();

        return order;
    }

    public static OrderProduct GenerateOrderProduct(string uniqueValue, Unit unit = null, decimal priceSubTotal = 0)
    {
        unit ??= new Unit();

        var orderProduct = new OrderProduct
        {
            Name = $"ProductOrder-{uniqueValue}",
            Unit = unit,
            Price = new Price { SubTotal = priceSubTotal }
        };
        
        orderProduct.ActualizeTotalPrice();

        return orderProduct;
    }
}