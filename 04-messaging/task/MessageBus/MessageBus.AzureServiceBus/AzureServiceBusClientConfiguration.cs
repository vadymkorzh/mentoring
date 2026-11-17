using Azure.Core;

namespace MessageBus.AzureServiceBus;

public class AzureServiceBusClientConfiguration
{
    public required string FullyQualifiedNamespace { get; init; }
    public required string ProductUpdateQueue { get; init; }
    public required TokenCredential Credential { get; init; }
}
