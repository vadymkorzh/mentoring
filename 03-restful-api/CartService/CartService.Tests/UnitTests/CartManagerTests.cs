using CartService.BLL;
using CartService.DAL;
using CartService.DAL.Models;
using Xunit;

namespace CartService.Tests.UnitTests;

public class CartManagerTests
{
    private sealed class FakeCartRepository : ICartRepository
    {
        private readonly Dictionary<string, Cart> _carts = [];

        public Task<Cart?> GetCartAsync(string key) =>
            Task.FromResult(_carts.TryGetValue(key, out Cart? cart) ? cart : null);

        public Task<bool> SaveCartAsync(Cart cart)
        {
            _carts[cart.Key] = cart;
            return Task.FromResult(true);
        }
    }

    [Fact]
    public async Task GetCartItemsAsync_ReturnsNull_WhenCartDoesNotExist()
    {
        var manager = new CartManager(new FakeCartRepository());

        IList<CartItem>? items = await manager.GetCartItemsAsync("missing-cart");

        Assert.Null(items);
    }

    [Fact]
    public async Task AddItemToCartAsync_CreatesCartAndAddsItem_WhenCartDoesNotExist()
    {
        var manager = new CartManager(new FakeCartRepository());
        var item = new CartItem { Id = 1, Name = "Widget", Price = 9.99m, Quantity = 2 };

        await manager.AddItemToCartAsync("new-cart", item);
        IList<CartItem>? items = await manager.GetCartItemsAsync("new-cart");

        Assert.NotNull(items);
        Assert.Equal(item, Assert.Single(items));
    }
}
