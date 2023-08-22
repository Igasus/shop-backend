using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Dto;
using Shop.Application.Services;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Owned;
using Shop.Domain.Exceptions;
using Shop.Infrastructure;
using Shop.Infrastructure.DataSources;
using Shop.Infrastructure.Repositories;
using Xunit;

namespace Shop.Tests.UnitTests;

public class CustomerServiceTests : IClassFixture<DbContextFixture>, IClassFixture<MapperFixture>
{
    private readonly ShopDbContext _dbContext;
    private readonly CustomerService _customerService;
    
    public CustomerServiceTests(DbContextFixture dbContextFixture, MapperFixture mapperFixture)
    {
        _dbContext = dbContextFixture.ShopDbContext;
        _dbContext.Database.EnsureCreated();
        
        var customerRepository = new CustomerRepository(_dbContext);
        var customerDataSource = new CustomerDataSource(_dbContext);
        var mapper = mapperFixture.Mapper;
        _customerService = new CustomerService(customerRepository, customerDataSource, mapper);
    }

    #region GetAllAsync

    [Fact]
    public async Task GetAllAsync_ReturnEmptyList()
    {
        try
        {
            // Act
            var getAllCustomersResult = await _customerService.GetAllAsync();
        
            // Assert
            Assert.Empty(getAllCustomersResult);
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
    public async Task GetAllAsync_ReturnList(int customersCount)
    {
        try
        {
            // Arrange
            var customers = new List<Customer>();
            for (var customerIndex = 0; customerIndex < customersCount; customerIndex++)
            {
                customers.Add(new Customer
                {
                    FullName = $"FullName-{customerIndex}",
                    PhoneNumber = customerIndex.ToString(),
                    Email = $"{customerIndex}@mail.com"
                });
            }

            await _dbContext.Customers.AddRangeAsync(customers);
            await _dbContext.SaveChangesAsync();
            
            // Act
            var getAllCustomersResult = await _customerService.GetAllAsync();
            
            // Assert
            Assert.Equal(customersCount, getAllCustomersResult.Count);
            foreach (var expectedCustomer in customers)
            {
                Assert.Contains(getAllCustomersResult, c => c.FullName == expectedCustomer.FullName);
                Assert.Contains(getAllCustomersResult, c => c.PhoneNumber == expectedCustomer.PhoneNumber);
                Assert.Contains(getAllCustomersResult, c => c.Email == expectedCustomer.Email);
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

    [Fact]
    public async Task GetByIdAsync_CorrectInput_ReturnCustomer()
    {
        try
        {
            // Arrange
            var expectedCustomer = new Customer
            {
                FullName = $"FullName-{Guid.NewGuid().ToString()}",
                PhoneNumber = Guid.NewGuid().ToString(),
                Email = $"{Guid.NewGuid().ToString()}@mail.com"
            };

            var entityEntry = await _dbContext.Customers.AddAsync(expectedCustomer);
            await _dbContext.SaveChangesAsync();
            entityEntry.State = EntityState.Detached;
            
            // Act
            var actualCustomer = await _customerService.GetByIdAsync(expectedCustomer.Id);
            
            // Assert
            Assert.NotNull(actualCustomer);
            Assert.Equal(expectedCustomer.Id, actualCustomer.Id);
            Assert.Equal(expectedCustomer.FullName, actualCustomer.FullName);
            Assert.Equal(expectedCustomer.PhoneNumber, actualCustomer.PhoneNumber);
            Assert.Equal(expectedCustomer.Email, actualCustomer.Email);
            Assert.Empty(actualCustomer.OrderIds);
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
    public async Task GetByIdAsync_CorrectInputWithOrders_ReturnCustomer(int ordersCount)
    {
        try
        {
            // Arrange
            var expectedOrders = new List<Order>();
            for (var orderIndex = 0; orderIndex < ordersCount; orderIndex++)
            {
                expectedOrders.Add(new Order
                {
                    Price = new Price(),
                    RequestedDiscount = new Discount(),
                    ResultDiscount = new Discount(),
                    Products = new List<OrderProduct>
                    {
                        new()
                        {
                            Name = orderIndex.ToString(),
                            Unit = new Unit
                            {
                                Quantity = 1,
                                Measure = "pt"
                            },
                            Price = new Price()
                        }
                    }
                });
            }
            
            var expectedCustomer = new Customer
            {
                FullName = $"FullName-{Guid.NewGuid().ToString()}",
                PhoneNumber = Guid.NewGuid().ToString(),
                Email = $"{Guid.NewGuid().ToString()}@mail.com",
                Orders = expectedOrders
            };

            var entityEntry = await _dbContext.Customers.AddAsync(expectedCustomer);
            await _dbContext.SaveChangesAsync();
            entityEntry.State = EntityState.Detached;
            
            // Act
            var actualCustomer = await _customerService.GetByIdAsync(expectedCustomer.Id);
            
            // Assert
            Assert.NotNull(actualCustomer);
            Assert.Equal(expectedCustomer.Id, actualCustomer.Id);
            Assert.Equal(expectedCustomer.FullName, actualCustomer.FullName);
            Assert.Equal(expectedCustomer.PhoneNumber, actualCustomer.PhoneNumber);
            Assert.Equal(expectedCustomer.Email, actualCustomer.Email);
            Assert.Equal(ordersCount, actualCustomer.OrderIds.Count);
            foreach (var expectedOrder in expectedCustomer.Orders)
            {
                Assert.Contains(actualCustomer.OrderIds, orderId => expectedOrder.Id == orderId);
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
            var incorrectCustomerId = Guid.NewGuid();

            // Act, Assert
            await Assert.ThrowsAsync<NotFoundException>(
                async () => await _customerService.GetByIdAsync(incorrectCustomerId));
        }
        finally
        {
            // Cleanup
            await _dbContext.Database.EnsureDeletedAsync();
        }
    }

    #endregion

    #region CreateAsync

    [Fact]
    public async Task CreateAsync_CorrectInput_ReturnId()
    {
        try
        {
            // Arrange
            var createCustomerInput = new CustomerDtoInput
            {
                FullName = $"FullName-{Guid.NewGuid().ToString()}",
                PhoneNumber = Guid.NewGuid().ToString(),
                Email = $"{Guid.NewGuid().ToString()}@mail.com"
            };
            
            // Act
            var customerId = await _customerService.CreateAsync(createCustomerInput);
            
            // Assert
            var actualCustomer = await _dbContext.Customers
                .Include(customer => customer.Orders)
                .FirstOrDefaultAsync(c => c.Id == customerId);
            
            Assert.NotNull(actualCustomer);
            Assert.Equal(createCustomerInput.FullName, actualCustomer.FullName);
            Assert.Equal(createCustomerInput.PhoneNumber, actualCustomer.PhoneNumber);
            Assert.Equal(createCustomerInput.Email, actualCustomer.Email);
            Assert.Empty(actualCustomer.Orders);
        }
        finally
        {
            // Cleanup
            await _dbContext.Database.EnsureDeletedAsync();
        }
    }

    #endregion
}