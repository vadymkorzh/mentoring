using Azure.Messaging.ServiceBus;
using MessageBus.Core;
using MessageBus.Core.Messages;
using System.Runtime.CompilerServices;

namespace MessageBus.AzureServiceBus;

internal class AzureServiceBusReceiver(ServiceBusReceiver receiver) : IMessageBusReceiver, IAsyncDisposable
{
    private readonly ServiceBusReceiver _receiver = receiver;
    private readonly List<ServiceBusReceivedMessage> _messages = [];

    public async IAsyncEnumerable<Message<ProductUpdate>> ReceiveProductUpdatesAsync([EnumeratorCancellation] CancellationToken token)
    {
        await foreach (ServiceBusReceivedMessage sbMessage in _receiver.ReceiveMessagesAsync(token))
        {
            _messages.Add(sbMessage);
            ProductUpdate? productUpdate = sbMessage.Body.ToObjectFromJson<ProductUpdate>();

            if (productUpdate != null)
            {
                Message<ProductUpdate> message = new()
                {
                    MessageId = sbMessage.MessageId,
                    Payload = productUpdate
                };
                yield return message;
            }
        }
    }

    public async Task CompleteMessageAsync(Message message)
    {
        ServiceBusReceivedMessage? sbMessage
            = _messages.FirstOrDefault(x => x.MessageId == message.MessageId);
        if (sbMessage != null)
        {
            await _receiver.CompleteMessageAsync(sbMessage);
            _messages.Remove(sbMessage);
        }
    }

    public async Task AbandonMessageAsync(Message message)
    {
        ServiceBusReceivedMessage? sbMessage
            = _messages.FirstOrDefault(x => x.MessageId == message.MessageId);
        if (sbMessage != null)
        {
            await _receiver.AbandonMessageAsync(sbMessage);
            _messages.Remove(sbMessage);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _receiver.DisposeAsync();
    }
}
