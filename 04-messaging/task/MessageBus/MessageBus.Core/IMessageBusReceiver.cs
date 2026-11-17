using MessageBus.Core.Messages;

namespace MessageBus.Core;

public interface IMessageBusReceiver
{
    IAsyncEnumerable<ProductUpdate> ReceiveProductUpdates();
}
