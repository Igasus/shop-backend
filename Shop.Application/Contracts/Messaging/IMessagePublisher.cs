using System.Threading.Tasks;

namespace Shop.Application.Contracts.Messaging;

public interface IMessagePublisher
{
    Task PublishAsync<T>(T obj);
}