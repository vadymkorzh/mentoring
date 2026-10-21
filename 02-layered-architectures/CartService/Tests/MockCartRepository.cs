using CartService.DAL;
using CartService.DAL.Models;

namespace CartService.Tests;

internal class MockCartRepository : ICartRepository
{
    private readonly Dictionary<int, Cart> _carts = [];

    public void AddCart(Cart cart) => _carts[cart.Id] = cart;

    public Task<Cart?> GetCartAsync(int cartId)
    {
        _carts.TryGetValue(cartId, out Cart? cart);
        return Task.FromResult(cart);
    }

    public Task<bool> SaveCartAsync(Cart cart)
    {
        _carts[cart.Id] = cart;
        return Task.FromResult(true);
    }
}
