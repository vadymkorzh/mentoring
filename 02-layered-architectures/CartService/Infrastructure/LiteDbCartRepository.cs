using CartService.DAL;
using CartService.DAL.Models;
using LiteDB;

namespace CartService.Infrastructure;

public class LiteDbCartRepository : ICartRepository
{
    public Task<Cart?> GetCartAsync(int cartId)
    {
        using var db = new LiteDatabase(@"\cart-service.db");
        var carts = db.GetCollection<Cart>("carts");

        return Task.FromResult<Cart?>(carts.Query().Where(x => x.Id == cartId).FirstOrDefault());
    }

    public Task<bool> SaveCartAsync(Cart cart)
    {
        using var db = new LiteDatabase(@"\cart-service.db");
        var carts = db.GetCollection<Cart>("carts");

        return Task.FromResult(carts.Upsert(cart));
    }
}
