using System.Text;
using DotNet.Testcontainers.Configurations;
using MessageBus.Core;
using MessageBus.Core.Messages;
using Testcontainers.ServiceBus;
using Xunit;

namespace MessageBus.AzureServiceBus.Tests.IntegrationTests;

public class AzureServiceBusClientTests : IAsyncLifetime
{
    private const string QueueName = "product-update-queue";

    private const string ConfigJson = """
        {
          "UserConfig": {
            "Namespaces": [
              {
                "Name": "sbemulatorns",
                "Queues": [
                  {
                    "Name": "product-update-queue",
                    "Properties": {
                      "DeadLetteringOnMessageExpiration": false,
                      "DefaultMessageTimeToLive": "PT1H",
                      "DuplicateDetectionHistoryTimeWindow": "PT20S",
                      "ForwardDeadLetteredMessagesTo": "",
                      "ForwardTo": "",
                      "LockDuration": "PT1M",
                      "MaxDeliveryCount": 10,
                      "RequiresDuplicateDetection": false,
                      "RequiresSession": false
                    }
                  }
                ],
                "Topics": []
              }
            ],
            "Logging": {
              "Type": "File"
            }
          }
        }
        """;

    private readonly ServiceBusContainer _container = new ServiceBusBuilder()
        .WithAcceptLicenseAgreement(true)
        .WithResourceMapping(Encoding.UTF8.GetBytes(ConfigJson), "/ServiceBus_Emulator/ConfigFiles/Config.json",
            UnixFileModes.UserRead | UnixFileModes.GroupRead | UnixFileModes.OtherRead)
        .Build();

    public Task InitializeAsync() => _container.StartAsync();

    public Task DisposeAsync() => _container.DisposeAsync().AsTask();

    [Fact]
    public async Task SendProductUpdateAsync_ThenReceive_RoundTripsThroughRealSenderAndReceiver()
    {
        var configuration = new AzureServiceBusClientConfiguration
        {
            ConnectionString = _container.GetConnectionString(),
            ProductUpdateQueue = QueueName
        };

        await using var client = new AzureServiceBusClient(configuration);
        IMessageBusSender sender = client.CreateSender();
        IMessageBusReceiver receiver = client.CreateReceiver();
        var update = new ProductUpdate { ProductId = 42, Name = "Chair", Price = 49.99m, Amount = 2 };

        await sender.SendProductUpdateAsync(update);

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        await using IAsyncEnumerator<Message<ProductUpdate>> enumerator =
            receiver.ReceiveProductUpdatesAsync(cts.Token).GetAsyncEnumerator();
        Assert.True(await enumerator.MoveNextAsync());
        Message<ProductUpdate> received = enumerator.Current;

        Assert.Equal(update.ProductId, received.Payload.ProductId);
        Assert.Equal(update.Name, received.Payload.Name);
        Assert.Equal(update.Price, received.Payload.Price);
        Assert.Equal(update.Amount, received.Payload.Amount);

        await receiver.CompleteMessageAsync(received);
    }
}
