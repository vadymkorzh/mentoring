using Asp.Versioning;
using CartService.BLL;
using CartService.DAL.Models;
using CartService.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Web.Controllers;

/// <summary>
/// Exposes operations for reading and mutating shopping carts.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/carts")]
public partial class CartController(CartManager cartManager) : ControllerBase
{
    private readonly CartManager _cartManager = cartManager;

    /// <summary>
    /// Gets the cart identified by <paramref name="cartKey"/>.
    /// </summary>
    /// <param name="cartKey">The unique key of the cart.</param>
    /// <returns>The cart and its items, or 404 if no cart exists for the given key.</returns>
    [HttpGet("{cartKey}")]
    [MapToApiVersion("1.0")]
    public async Task<IActionResult> GetCartInfoV1(string cartKey)
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

    /// <summary>
    /// Adds an item to the cart identified by <paramref name="cartKey"/>, creating the cart if it doesn't exist yet.
    /// </summary>
    /// <param name="cartKey">The unique key of the cart.</param>
    /// <param name="item">The item to add to the cart.</param>
    /// <returns>201 Created with the added item and a Location header pointing to the cart.</returns>
    [HttpPost("{cartKey}/items")]
    [MapToApiVersion("1.0")]
    public async Task<IActionResult> AddItemToCartV1(string cartKey, [FromBody] CartItemDto item)
    {
        await _cartManager.AddItemToCartAsync(cartKey, item.ToModel());

        return CreatedAtAction(nameof(GetCartInfoV1), new { cartKey }, item);
    }

    /// <summary>
    /// Removes an item from the cart identified by <paramref name="cartKey"/>.
    /// </summary>
    /// <param name="cartKey">The unique key of the cart.</param>
    /// <param name="itemId">The id of the item to remove.</param>
    /// <returns>204 No Content if the item was removed, or 404 if the cart or item doesn't exist.</returns>
    [HttpDelete("{cartKey}/items/{itemId:int}")]
    public async Task<IActionResult> DeleteItemFromCart(string cartKey, int itemId)
    {
        bool removed = await _cartManager.RemoveItemFromCartAsync(cartKey, itemId);

        return removed ? NoContent() : NotFound();
    }
}
