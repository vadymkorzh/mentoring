using CartService.DAL;
using CartService.DAL.Models;

namespace CartService.BLL;

public class CartService(ICartRepository cartRepository) : ICartService
{
    private readonly ICartRepository _cartRepository = cartRepository;

    public async Task<IList<CartItem>?> GetCartItemsAsync(int cartId)
    {
        Cart? cart = await _cartRepository.GetCartAsync(cartId);
        
        return cart?.GetItems();
    }

    public async Task AddItemToCartAsync(int cartId, CartItem item)
    {
        Cart? cart = await _cartRepository.GetCartAsync(cartId);
        if (cart is not null)
        {
            cart.AddItem(item);
            await _cartRepository.SaveCartAsync(cart);
        }
    }

    public async Task<bool> RemoveItemFromCartAsync(int cartId, int itemId)
    {
        Cart? cart = await _cartRepository.GetCartAsync(cartId);
        if (cart is not null && cart.RemoveItem(itemId))
        {
            await _cartRepository.SaveCartAsync(cart);
            return true;
        }

        return false;
    }
}
