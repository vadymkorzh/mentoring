using Azure.Messaging.ServiceBus;
using MessageBus.Core;

namespace MessageBus.AzureServiceBus;

public class AzureServiceBusClient : IMessageBusClient, IAsyncDisposable
{
    private readonly ServiceBusClient _sdkClient;
    private readonly AzureServiceBusClientConfiguration _configuration;
    private AzureServiceBusSender? _sender;
    private AzureServiceBusReceiver? _receiver;

    public AzureServiceBusClient(AzureServiceBusClientConfiguration configuration)
    {
        _sdkClient = configuration.ConnectionString is not null
            ? new ServiceBusClient(configuration.ConnectionString)
            : new ServiceBusClient(configuration.FullyQualifiedNamespace, configuration.Credential,
                new ServiceBusClientOptions
                {
                    TransportType = ServiceBusTransportType.AmqpWebSockets
                });

        _configuration = configuration;
    }

    public IMessageBusSender CreateSender()
    {
        if (_sender == null)
        {
            ServiceBusSender sender = _sdkClient.CreateSender(_configuration.ProductUpdateQueue);
            _sender = new AzureServiceBusSender(sender);
        }

        return _sender;
    }

    public IMessageBusReceiver CreateReceiver()
    {
        if (_receiver == null)
        {
            ServiceBusReceiver receiver = _sdkClient.CreateReceiver(_configuration.ProductUpdateQueue);
            _receiver = new AzureServiceBusReceiver(receiver);
        }

        return _receiver;
    }

    public async ValueTask DisposeAsync()
    {
        if (_sender != null)
        {
            await _sender.DisposeAsync();
        }

        if (_receiver != null)
        {
            await _receiver.DisposeAsync();
        }

        await _sdkClient.DisposeAsync();

        GC.SuppressFinalize(this);
    }
}
