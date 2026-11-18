namespace MessageBus.Core.Messages;

public abstract class MessageBase
{
    public required string MessageId { get; init; }
}
