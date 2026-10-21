using CartService.DAL.Models;
using Xunit;

namespace CartService.Tests;

public class CartServiceUnitTests
{
    private static CartItem CreateItem(int id) => new()
    {
        Id = id,
        Name = "Item",
        Price = 9.99m,
        Quantity = 1
    };

    [Fact]
    public async Task GetCartItemsAsync_CartExists_ReturnsItems()
    {
        var repository = new MockCartRepository();
        var cart = new Cart(1);
        cart.AddItem(CreateItem(1));
        repository.AddCart(cart);
        var sut = new BLL.CartService(repository);

        IList<CartItem>? items = await sut.GetCartItemsAsync(1);

        Assert.NotNull(items);
        Assert.Single(items);
        Assert.Equal(1, items[0].Id);
    }

    [Fact]
    public async Task GetCartItemsAsync_CartDoesNotExist_ReturnsNull()
    {
        var repository = new MockCartRepository();
        var sut = new BLL.CartService(repository);

        IList<CartItem>? items = await sut.GetCartItemsAsync(42);

        Assert.Null(items);
    }
}
