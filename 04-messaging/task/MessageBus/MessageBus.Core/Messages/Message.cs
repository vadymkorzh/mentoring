namespace MessageBus.Core.Messages;

public class Message
{
    public required string MessageId { get; init; }
}

public class Message<T> : Message where T: MessagePayload
{
    public required T Payload { get; init; }
}
