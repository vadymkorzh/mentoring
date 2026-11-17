using MessageBus.Core.Messages;

namespace MessageBus.Core;

public interface IMessageBusSender
{
    Task SendProductUpdate(ProductUpdate update);
}
