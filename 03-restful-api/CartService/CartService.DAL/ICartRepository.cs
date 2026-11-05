using CartService.DAL.Models;

namespace CartService.DAL;

public interface ICartRepository
{
    public Task<Cart?> GetCartAsync(string key);

    public Task<bool> SaveCartAsync(Cart cart);
}
