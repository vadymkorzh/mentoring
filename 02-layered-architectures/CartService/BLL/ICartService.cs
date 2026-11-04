using CartService.DAL.Models;

namespace CartService.BLL;

public interface ICartService
{
    Task<IList<CartItem>?> GetCartItemsAsync(int cartId);
    Task AddItemToCartAsync(int cartId, CartItem item);
    Task<bool> RemoveItemFromCartAsync(int cartId, int itemId);
}