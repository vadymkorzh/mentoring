using Azure.Messaging.ServiceBus;
using MessageBus.Core;
using MessageBus.Core.Messages;

namespace MessageBus.AzureServiceBus;

internal class AzureServiceBusSender(ServiceBusSender sender) : IMessageBusSender, IAsyncDisposable
{
    private readonly ServiceBusSender _sender = sender;

    public async Task SendProductUpdate(ProductUpdate update)
    {
        var message = new ServiceBusMessage(new BinaryData(update));
        try
        {
            await _sender.SendMessageAsync(message);
        }
        catch (ServiceBusException)
        {
            throw;
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _sender.DisposeAsync();
    }
}
