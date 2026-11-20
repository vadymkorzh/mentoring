using Azure.Core;
using MessageBus.AzureServiceBus;
using MessageBus.Core;
using Xunit;

namespace MessageBus.Tests;

public class AzureServiceBusClientTests
{
    [Fact]
    public void CreateSender_CalledTwice_ReturnsSameInstance()
    {
        var client = CreateClient();

        IMessageBusSender first = client.CreateSender();
        IMessageBusSender second = client.CreateSender();

        Assert.Same(first, second);
    }

    [Fact]
    public void CreateReceiver_CalledTwice_ReturnsSameInstance()
    {
        var client = CreateClient();

        IMessageBusReceiver first = client.CreateReceiver();
        IMessageBusReceiver second = client.CreateReceiver();
        Assert.Same(first, second);
    }

    private static AzureServiceBusClient CreateClient() => new(new AzureServiceBusClientConfiguration
    {
        FullyQualifiedNamespace = "test.servicebus.windows.net",
        ProductUpdateQueue = "test-queue",
        Credential = new FakeTokenCredential()
    });

    private sealed class FakeTokenCredential : TokenCredential
    {
        public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken) =>
            new("fake-token", DateTimeOffset.UtcNow.AddHours(1));

        public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken) =>
            ValueTask.FromResult(GetToken(requestContext, cancellationToken));
    }
}
