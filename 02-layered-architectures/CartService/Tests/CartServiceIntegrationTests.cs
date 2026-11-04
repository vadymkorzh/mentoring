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
        string databasePath = Path.Combine(Path.GetTempPath(), $"cart-service-{Guid.NewGuid()}.db");

        try
        {
            var seed = new LiteDbCartRepository(databasePath);
            await seed.SaveCartAsync(new Cart(TestCartId));

            var sut = new BLL.CartService(new LiteDbCartRepository(databasePath));
            await sut.AddItemToCartAsync(TestCartId, CreateItem(1));

            var verifier = new BLL.CartService(new LiteDbCartRepository(databasePath));
            IList<CartItem>? items = await verifier.GetCartItemsAsync(TestCartId);

            Assert.NotNull(items);
            Assert.Single(items);
            Assert.Equal(1, items[0].Id);
        }
        finally
        {
            File.Delete(databasePath);
        }
    }
}
