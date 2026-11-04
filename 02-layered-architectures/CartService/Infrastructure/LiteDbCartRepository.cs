using CartService.DAL;
using CartService.DAL.Models;
using LiteDB;

namespace CartService.Infrastructure;

public class LiteDbCartRepository(string databasePath = "cart-service.db") : ICartRepository
{
    private readonly string _databasePath = databasePath;

    static LiteDbCartRepository()
    {
        BsonMapper.Global.RegisterType<Cart>(
            cart => new BsonDocument
            {
                ["_id"] = cart.Id,
                ["items"] = new BsonArray(cart.GetItems().Select(item => BsonMapper.Global.ToDocument(item)))
            },
            doc =>
            {
                var cart = new Cart(doc["_id"].AsInt32);
                foreach (BsonValue itemValue in doc["items"].AsArray)
                {
                    cart.AddItem(BsonMapper.Global.ToObject<CartItem>(itemValue.AsDocument));
                }

                return cart;
            });
    }

    public Task<Cart?> GetCartAsync(int cartId)
    {
        using var db = new LiteDatabase(_databasePath);
        var carts = db.GetCollection<Cart>("carts");

        return Task.FromResult<Cart?>(carts.Query().Where(x => x.Id == cartId).FirstOrDefault());
    }

    public Task<bool> SaveCartAsync(Cart cart)
    {
        using var db = new LiteDatabase(_databasePath);
        var carts = db.GetCollection<Cart>("carts");

        return Task.FromResult(carts.Upsert(cart));
    }
}
