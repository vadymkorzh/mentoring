using Azure.Messaging.ServiceBus;
using MessageBus.Core;
using MessageBus.Core.Messages;

namespace MessageBus.AzureServiceBus;

internal class AzureServiceBusReceiver(ServiceBusReceiver receiver) : IMessageBusReceiver, IAsyncDisposable
{
    private readonly ServiceBusReceiver _receiver = receiver;

    public async IAsyncEnumerable<ProductUpdate> ReceiveProductUpdates()
    {
        ServiceBusReceivedMessage message = await _receiver.ReceiveMessageAsync();
        ProductUpdate? productUpdate = message.Body.ToObjectFromJson<ProductUpdate>();

        if (productUpdate != null)
        {
            yield return productUpdate;
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _receiver.DisposeAsync();
    }
}
