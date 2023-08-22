using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using Shop.Application.Contracts.Messaging;
using Shop.Infrastructure.Options;

namespace Shop.Infrastructure.Messaging;

public class AzureServiceBusMessagePublisher : IMessagePublisher
{
    private readonly ServiceBusSender _serviceBusSender;

    public AzureServiceBusMessagePublisher(ServiceBusClient serviceBusClient, IOptions<AzureOptions> options)
    {
        _serviceBusSender = serviceBusClient.CreateSender(options.Value.ServiceBus.QueueOrTopicName);
    }

    public async Task PublishAsync<T>(T obj)
    {
        var objAsJson = JsonSerializer.Serialize(obj);
        var message = new ServiceBusMessage(objAsJson);
        await _serviceBusSender.SendMessageAsync(message);
    }
}