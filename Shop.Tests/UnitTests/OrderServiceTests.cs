using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shop.Application.Contracts.Messaging;
using Shop.Application.Dto;
using Shop.Application.Services;
using Shop.Domain.Entities;
using Shop.Domain.Exceptions;
using Shop.Infrastructure;
using Shop.Infrastructure.DataSources;
using Shop.Infrastructure.Repositories;
using Shop.Tests.DataGenerators;
using Xunit;

// Disabled warnings: DbContext.DbSet.Include is not necessary for owned entities.
// ReSharper disable EntityFramework.NPlusOne.IncompleteDataUsage
// ReSharper disable EntityFramework.NPlusOne.IncompleteDataQuery

namespace Shop.Tests.UnitTests;

public class OrderServiceTests : IClassFixture<DbContextFixture>, IClassFixture<MapperFixture>
{
    private readonly ShopDbContext _dbContext;
    private readonly OrderService _orderService;
    
    public OrderServiceTests(DbContextFixture dbContextFixture, MapperFixture mapperFixture)
    {
        _dbContext = dbContextFixture.ShopDbContext;
        _dbContext.Database.EnsureCreated();

        var orderRepository = new OrderRepository(_dbContext);
        var orderDataSource = new OrderDataSource(_dbContext);
        var customerDataSource = new CustomerDataSource(_dbContext);
        var mapper = mapperFixture.Mapper;

        var messagePublisherMock = new Mock<IMessagePublisher>();
        messagePublisherMock
            .Setup(publisher => publisher.PublishAsync(It.IsAny<It.IsAnyType>()))
            .Returns(Task.CompletedTask);
        var messagePublisher = messagePublisherMock.Object;

        _orderService =
            new OrderService(orderRepository, orderDataSource, customerDataSource, mapper, messagePublisher);
    }

    #region GetAllAsync

    [Fact]
    public async Task GetAllAsync_NoOrders_ReturnEmptyList()
    {
        try
        {
            // Act
            var getAllOrdersResult = await _orderService.GetAllAsync();
            
            // Assert
            Assert.Empty(getAllOrdersResult);
        }
        finally
        {
            // Cleanup
            await _dbContext.Database.EnsureDeletedAsync();
        }
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    public async Task GetAllAsync_ReturnList(int ordersCount)
    {
        try
        {
            // Arrange
            var customer = TestEntityGenerator.GenerateCustomer(Guid.NewGuid().ToString());
            for (var orderIndex = 0; orderIndex < ordersCount; orderIndex++)
            {
                var orderProducts = new List<OrderProduct>
                    { TestEntityGenerator.GenerateOrderProduct(orderIndex.ToString()) };
                customer.Orders.Add(TestEntityGenerator.GenerateOrder(products: orderProducts));
            }

            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            
            // Act
            var actualOrderList = await _orderService.GetAllAsync();
            
            // Assert
            Assert.Equal(ordersCount, actualOrderList.Count);
            foreach (var expectedOrder in customer.Orders)
            {
                var actualOrder = actualOrderList.FirstOrDefault(o => o.Id == expectedOrder.Id);
                Assert.NotNull(actualOrder);
                Assert.Equal(expectedOrder.CustomerId, actualOrder.CustomerId);
                Assert.Equal(expectedOrder.Products.Count, actualOrder.Products.Count);
                foreach (var expectedOrderProduct in expectedOrder.Products)
                {
                    Assert.Contains(actualOrder.Products, orderProduct =>
                        orderProduct.Name == expectedOrderProduct.Name);
                }
            }
        }
        finally
        {
            // Cleanup
            await _dbContext.Database.EnsureDeletedAsync();
        }
    }

    #endregion

    #region GetByIdAsync

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    public async Task GetByIdAsync_CorrectInput_ReturnOrder(int orderProductsCount)
    {
        try
        {
            // Arrange
            var expectedOrderProductList = new List<OrderProduct>();
            for (var orderProductIndex = 0; orderProductIndex < orderProductsCount; orderProductIndex++)
            {
                expectedOrderProductList.Add(TestEntityGenerator.GenerateOrderProduct(orderProductIndex.ToString()));
            }
            var expectedOrder = TestEntityGenerator.GenerateOrder(products: expectedOrderProductList);

            await _dbContext.Orders.AddAsync(expectedOrder);
            await _dbContext.SaveChangesAsync();
            
            // Act
            var actualOrder = await _orderService.GetByIdAsync(expectedOrder.Id);
            
            // Assert
            Assert.NotNull(actualOrder);
            Assert.Equal(orderProductsCount, actualOrder.Products.Count);
            foreach (var expectedOrderProduct in expectedOrderProductList)
            {
                var actualOrderProduct = actualOrder.Products
                    .FirstOrDefault(orderProduct => orderProduct.Id == expectedOrderProduct.Id);
                Assert.NotNull(actualOrderProduct);
                Assert.Equal(expectedOrderProduct.Name, actualOrderProduct.Name);
            }
        }
        finally
        {
            // Cleanup
            await _dbContext.Database.EnsureDeletedAsync();
        }
    }

    [Fact]
    public async Task GetByIdAsync_IncorrectId_ThrowNotFound()
    {
        try
        {
            // Arrange
            var incorrectId = Guid.NewGuid();
        
            // Act, Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await _orderService.GetByIdAsync(incorrectId));
        }
        finally
        {
            // Cleanup
            await _dbContext.Database.EnsureDeletedAsync();
        }
    }

    #endregion

    #region CreateAsync

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    public async Task CreateAsync_CorrectInput_ReturnId(int orderProductsCount)
    {
        try
        {
            // Arrange
            var customer = TestEntityGenerator.GenerateCustomer(Guid.NewGuid().ToString());
            
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            
            var orderProductCreateInputList = new List<OrderProductDtoInput>();
            for (var orderProductIndex = 0; orderProductIndex < orderProductsCount; orderProductIndex++)
            {
                orderProductCreateInputList.Add(new OrderProductDtoInput
                {
                    Name = Guid.NewGuid().ToString(),
                    UnitQuantity = TestValueGenerator.GetRandomDecimal(1, 10),
                    UnitMeasure = Guid.NewGuid().ToString(),
                    PriceSubTotal = TestValueGenerator.GetRandomDecimal(1000, 10000)
                });
            }
            var orderCreateInput = new OrderDtoInput
            {
                CustomerId = customer.Id,
                RequestedDiscountPercent = TestValueGenerator.GetRandomDecimal(1, 50),
                RequestedDiscountValue = TestValueGenerator.GetRandomDecimal(100, 500),
                Products = orderProductCreateInputList
            };
            
            // Act
            var orderId = await _orderService.CreateAsync(orderCreateInput);
            
            // Assert
            var actualOrder = await _dbContext.Orders
                .Include(order => order.Products)
                .FirstOrDefaultAsync(order => order.Id == orderId);
            
            Assert.NotNull(actualOrder);
            Assert.Equal(orderCreateInput.RequestedDiscountPercent, actualOrder.RequestedDiscount.Percent);
            Assert.Equal(orderCreateInput.RequestedDiscountValue, actualOrder.RequestedDiscount.Value);
            Assert.Equal(orderProductsCount, actualOrder.Products.Count);
            
            foreach (var expectedOrderProduct in orderCreateInput.Products)
            {
                var actualOrderProduct = actualOrder.Products
                    .FirstOrDefault(orderProduct => orderProduct.Name == expectedOrderProduct.Name);
                Assert.NotNull(actualOrderProduct);
                Assert.Equal(expectedOrderProduct.UnitQuantity, actualOrderProduct.Unit.Quantity);
                Assert.Equal(expectedOrderProduct.UnitMeasure, actualOrderProduct.Unit.Measure);
                Assert.Equal(expectedOrderProduct.PriceSubTotal, actualOrderProduct.Price.SubTotal);
            }
        }
        finally
        {
            // Cleanup
            await _dbContext.Database.EnsureDeletedAsync();
        }
    }

    #endregion
}