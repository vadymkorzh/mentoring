using Azure.Core;

namespace MessageBus.AzureServiceBus;

public class AzureServiceBusClientConfiguration
{
    public string? FullyQualifiedNamespace { get; init; }
    public required string ProductUpdateQueue { get; init; }
    public TokenCredential? Credential { get; init; }
    public string? ConnectionString { get; init; }
}
