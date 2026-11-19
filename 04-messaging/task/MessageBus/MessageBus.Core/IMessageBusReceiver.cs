using MessageBus.Core.Messages;

namespace MessageBus.Core;

public interface IMessageBusReceiver
{
    IAsyncEnumerable<Message<ProductUpdate>> ReceiveProductUpdatesAsync(CancellationToken token = CancellationToken.None);
    Task CompleteMessageAsync(Message message);
    Task AbandonMessageAsync(Message message);
}
