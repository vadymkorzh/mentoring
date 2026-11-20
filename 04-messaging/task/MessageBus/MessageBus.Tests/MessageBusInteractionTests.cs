using System.Threading.Channels;
using MessageBus.Core;
using MessageBus.Core.Messages;
using Xunit;

namespace MessageBus.Tests;

public class MessageBusInteractionTests
{
    [Fact]
    public async Task SentUpdate_IsReceivedAndCanBeCompleted()
    {
        var bus = new InMemoryMessageBus();
        IMessageBusSender sender = bus.CreateSender();
        IMessageBusReceiver receiver = bus.CreateReceiver();
        var update = new ProductUpdate { ProductId = 7, Name = "Chair", Price = 49.99m, Amount = 2 };

        await sender.SendProductUpdateAsync(update);

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await using IAsyncEnumerator<Message<ProductUpdate>> enumerator =
            receiver.ReceiveProductUpdatesAsync(cts.Token).GetAsyncEnumerator();
        Assert.True(await enumerator.MoveNextAsync());
        Message<ProductUpdate> received = enumerator.Current;

        Assert.Equal(update.ProductId, received.Payload.ProductId);
        Assert.Equal(update.Name, received.Payload.Name);

        await receiver.CompleteMessageAsync(received);

        Assert.Contains(received.MessageId, bus.CompletedMessageIds);
    }

    private sealed class InMemoryMessageBus : IMessageBusClient
    {
        private readonly Channel<Message<ProductUpdate>> _channel = Channel.CreateUnbounded<Message<ProductUpdate>>();

        public List<string> CompletedMessageIds { get; } = [];

        public IMessageBusSender CreateSender() => new Sender(_channel.Writer);

        public IMessageBusReceiver CreateReceiver() => new Receiver(_channel.Reader, CompletedMessageIds);

        private sealed class Sender(ChannelWriter<Message<ProductUpdate>> writer) : IMessageBusSender
        {
            public Task SendProductUpdateAsync(ProductUpdate update)
            {
                var message = new Message<ProductUpdate> { MessageId = Guid.NewGuid().ToString(), Payload = update };

                return writer.WriteAsync(message).AsTask();
            }
        }

        private sealed class Receiver(ChannelReader<Message<ProductUpdate>> reader, List<string> completedMessageIds)
            : IMessageBusReceiver
        {
            public IAsyncEnumerable<Message<ProductUpdate>> ReceiveProductUpdatesAsync(CancellationToken token = default) =>
                reader.ReadAllAsync(token);

            public Task CompleteMessageAsync(Message message)
            {
                completedMessageIds.Add(message.MessageId);

                return Task.CompletedTask;
            }

            public Task AbandonMessageAsync(Message message) => Task.CompletedTask;
        }
    }
}
