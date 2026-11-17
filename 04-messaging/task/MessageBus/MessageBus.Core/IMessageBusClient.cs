namespace MessageBus.Core;

public interface IMessageBusClient
{
    IMessageBusReceiver CreateSender();
    IMessageBusReceiver CreateReceiver();
}
