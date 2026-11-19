using CartService.DAL.Models;
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
        IMessageBusClient messageBusClient,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _receiver = messageBusClient.CreateReceiver();
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Product update listener started.");

        try
        {
            await foreach (Message<ProductUpdate> message in _receiver.ReceiveProductUpdatesAsync(stoppingToken)
                .WithCancellation(stoppingToken))
            {
                try
                {
                    using IServiceScope scope = _serviceProvider.CreateScope();
                    var cartManager = scope.ServiceProvider.GetRequiredService<CartManager>();

                    IList<Cart> carts = await cartManager.GetAllCartsAsync();
                    foreach (Cart cart in carts)
                    {
                        await UpdateCart(cartManager, cart, message.Payload);
                    }

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

    private static async Task UpdateCart(CartManager manager, Cart cart, ProductUpdate productUpdate)
    {
        int productId = productUpdate.ProductId;
        foreach (CartItem item in cart.GetItems())
        {
            if (item.Id == productId)
            {
                CartItem newItem = new()
                {
                    Id = item.Id,
                    Image = item.Image,
                    Name = productUpdate.Name ?? item.Name,
                    Price = productUpdate.Price ?? item.Price,
                    Quantity = productUpdate.Amount ?? item.Quantity
                };
                await manager.RemoveItemFromCartAsync(cart.Key, item.Id);
                await manager.AddItemToCartAsync(cart.Key, newItem);
            }
        }
    }
}
