using CartService.BLL;
using CartService.DAL.Models;
using CartService.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Web.Controllers;

[ApiController]
[Route("carts")]
public class CartController(CartManager cartManager) : ControllerBase
{
    private readonly CartManager _cartManager = cartManager;

    [HttpGet("{cartKey}")]
    public async Task<IActionResult> GetCartInfo(string cartKey)
    {
        IList<CartItem>? items = await _cartManager.GetCartItemsAsync(cartKey);
        if (items is null)
        {
            return NotFound();
        }

        var cart = new CartDto
        {
            Key = cartKey,
            Items = items.Select(item => item.ToDto()).ToList()
        };

        return Ok(cart);
    }

    [HttpPost("{cartKey}/items")]
    public async Task<IActionResult> AddItemToCart(string cartKey, [FromBody] CartItemDto item)
    {
        await _cartManager.AddItemToCartAsync(cartKey, item.ToModel());

        return CreatedAtAction(nameof(GetCartInfo), new { cartKey }, item);
    }

    [HttpDelete("{cartKey}/items/{itemId:int}")]
    public async Task<IActionResult> DeleteItemFromCart(string cartKey, int itemId)
    {
        bool removed = await _cartManager.RemoveItemFromCartAsync(cartKey, itemId);

        return removed ? NoContent() : NotFound();
    }
}
