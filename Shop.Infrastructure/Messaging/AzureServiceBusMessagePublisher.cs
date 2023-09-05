using System;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shop.Application.Contracts.Messaging;
using Shop.Infrastructure.Options;

namespace Shop.Infrastructure.Messaging;

public class AzureServiceBusMessagePublisher : IMessagePublisher, IAsyncDisposable
{
    private readonly AzureOptions.ServiceBusOptions _options;
    private readonly ILogger<AzureServiceBusMessagePublisher> _logger;
    
    private ServiceBusSender _serviceBusSender;
    private bool _isSenderInitialized;

    public AzureServiceBusMessagePublisher(
        IOptions<AzureOptions> options,
        ILogger<AzureServiceBusMessagePublisher> logger)
    {
        _options = options.Value.ServiceBus;
        _logger = logger;
        
        TryInitializeSender();
    }

    public async Task PublishAsync<T>(T obj)
    {
        if (!_isSenderInitialized && !TryInitializeSender())
        {
            _logger.LogError(
                "Error while sending message to Azure ServiceBus: Azure ServiceBusSender is not initialized.");
            return;
        }
        
        var objAsJson = JsonSerializer.Serialize(obj);
        var message = new ServiceBusMessage(objAsJson);
        await _serviceBusSender.SendMessageAsync(message);
    }

    private bool TryInitializeSender()
    {
        try
        {
            var serviceBusClient = new ServiceBusClient(_options.ConnectionString);
            _serviceBusSender = serviceBusClient.CreateSender(_options.QueueOrTopicName);
            _isSenderInitialized = true;
            _logger.LogInformation("Azure ServiceBusSender initializer successfully");
        }
        catch (Exception exception)
        {
            _isSenderInitialized = false;
            _logger.LogError(exception, "Error while initializing Azure ServiceBusSender.");
        }

        return _isSenderInitialized;
    }

    public async ValueTask DisposeAsync()
    {
        if (_isSenderInitialized && _serviceBusSender is not null)
        {
            await _serviceBusSender.DisposeAsync();
            _isSenderInitialized = false;
        }
    }
}