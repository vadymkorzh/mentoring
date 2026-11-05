using CartService.DAL;
using CartService.DAL.Models;

namespace CartService.BLL;

public class CartManager(ICartRepository cartRepository)
{
    private readonly ICartRepository _cartRepository = cartRepository;

    public async Task<IList<CartItem>?> GetCartItemsAsync(string cartKey)
    {
        Cart? cart = await _cartRepository.GetCartAsync(cartKey);
        
        return cart?.GetItems();
    }

    public async Task AddItemToCartAsync(string cartKey, CartItem item)
    {
        Cart? cart = await _cartRepository.GetCartAsync(cartKey);
        if (cart is not null)
        {
            cart.AddItem(item);
            await _cartRepository.SaveCartAsync(cart);
        }
    }

    public async Task<bool> RemoveItemFromCartAsync(string cartKey, int itemId)
    {
        Cart? cart = await _cartRepository.GetCartAsync(cartKey);
        if (cart is not null && cart.RemoveItem(itemId))
        {
            await _cartRepository.SaveCartAsync(cart);
            return true;
        }

        return false;
    }
}
