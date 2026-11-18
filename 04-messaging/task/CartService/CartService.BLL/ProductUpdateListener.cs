using MessageBus.Core;
using MessageBus.Core.Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CartService.BLL;

public class ProductUpdateListener : BackgroundService
{
    private readonly ILogger<ProductUpdateListener> _logger;
    private readonly IMessageBusReceiver _receiver;
    private readonly IServiceProvider _serviceProvider;

    public ProductUpdateListener(
        ILogger<ProductUpdateListener> logger,
        IMessageBusReceiver receiver,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _receiver = receiver;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Product update listener started.");

        try
        {
            await foreach (ProductUpdate? update in _receiver.ReceiveProductUpdates().WithCancellation(stoppingToken))
            {
                try
                {
                    if (update != null)
                    {
                        using IServiceScope scope = _serviceProvider.CreateScope();
                        var cartManager = scope.ServiceProvider.GetRequiredService<CartManager>();
                        // update carts
                    }
                    // complete message
                }
                catch (Exception)
                {
                    _logger.LogError("Error processing catalog update message {MessageID}", update.MessageId);
                    // abandon message
                }
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Product update listener stopped.");
        }
    }
}
