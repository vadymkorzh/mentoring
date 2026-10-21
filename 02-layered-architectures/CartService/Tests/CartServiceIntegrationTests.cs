using CartService.DAL.Models;
using CartService.Infrastructure;
using Xunit;

namespace CartService.Tests;

public class CartServiceIntegrationTests
{
    private const int TestCartId = 100;

    private static CartItem CreateItem(int id) => new()
    {
        Id = id,
        Name = "Item",
        Price = 9.99m,
        Quantity = 1
    };

    [Fact]
    public async Task AddItemToCartAsync_RealRepository_ItemIsPersistedAndRetrievableFromNewConnection()
    {
        var seed = new LiteDbCartRepository();
        await seed.SaveCartAsync(new Cart(TestCartId));

        try
        {
            var sut = new BLL.CartService(new LiteDbCartRepository());
            await sut.AddItemToCartAsync(TestCartId, CreateItem(1));

            var verifier = new BLL.CartService(new LiteDbCartRepository());
            IList<CartItem>? items = await verifier.GetCartItemsAsync(TestCartId);

            Assert.NotNull(items);
            Assert.Single(items);
            Assert.Equal(1, items[0].Id);
        }
        finally
        {
            await seed.SaveCartAsync(new Cart(TestCartId));
        }
    }
}
