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
            await foreach (Message<ProductUpdate> message in _receiver.ReceiveProductUpdatesAsync()
                .WithCancellation(stoppingToken))
            {
                try
                {
                    using IServiceScope scope = _serviceProvider.CreateScope();
                    var cartManager = scope.ServiceProvider.GetRequiredService<CartManager>();
                    // update carts

                    await _receiver.CompleteMessageAsync(message);
                }
                catch (Exception)
                {
                    _logger.LogError("Error processing catalog update message {MessageID}", message.MessageId);
                    await _receiver.AbandonMessageAsync(message);
                }
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Product update listener stopped.");
        }
    }
}
