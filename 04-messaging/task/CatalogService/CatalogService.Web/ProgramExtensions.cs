using Azure.Identity;
using MessageBus.AzureServiceBus;
using MessageBus.Core;

namespace Microsoft.Extensions.DependencyInjection;

public static class ProgramExtensions
{
    public static void AddMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        IConfigurationSection azureServiceBusConfig = configuration.GetSection("AzureServiceBus");

        if (azureServiceBusConfig.Exists())
        {
            string? @namespace = azureServiceBusConfig.GetValue<string>("Namespace");
            string? productUpdateQueue = azureServiceBusConfig.GetValue<string>("ProductUpdateQueue");

            if (@namespace != null && productUpdateQueue != null)
            {
                var clientConfig = new AzureServiceBusClientConfiguration()
                {
                    FullyQualifiedNamespace = @namespace,
                    ProductUpdateQueue = productUpdateQueue,
                    Credential = new AzureCliCredential()
                };
                services.AddSingleton<IMessageBusClient, AzureServiceBusClient>(services => new AzureServiceBusClient(clientConfig));

                return;
            }
        }

        throw new Exception("Message bus is not added.");
    }
}
