using System.Threading.Tasks;
using Moq;
using Shop.Application.Contracts.Messaging;
using Shop.Application.Services;
using Shop.Infrastructure;
using Shop.Infrastructure.DataSources;
using Shop.Infrastructure.Repositories;
using Xunit;

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

    

    #endregion

    #region GetByIdAsync

    

    #endregion

    #region CreateAsync

    

    #endregion
}