namespace MessageBus.Core;

public interface IMessageBusClient
{
    IMessageBusSender CreateSender();
    IMessageBusReceiver CreateReceiver();
}
