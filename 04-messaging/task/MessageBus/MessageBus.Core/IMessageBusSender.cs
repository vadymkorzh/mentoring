using MessageBus.Core.Messages;

namespace MessageBus.Core;

public interface IMessageBusSender
{
    Task SendItemUpdate(ProductUpdate update);
}
