using CartService.DAL.Models;

namespace CartService.DAL;

public interface ICartRepository
{
    public Task<Cart?> GetCartAsync(int cartId);

    public Task<bool> SaveCartAsync(Cart cart);
}
