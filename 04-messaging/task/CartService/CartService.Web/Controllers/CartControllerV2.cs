using Asp.Versioning;
using CartService.DAL.Models;
using CartService.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Web.Controllers;

public partial class CartController
{
    /// <summary>
    /// Gets the raw list of items in the cart identified by <paramref name="cartKey"/>.
    /// </summary>
    /// <param name="cartKey">The unique key of the cart.</param>
    /// <returns>The cart's items, or 404 if no cart exists for the given key.</returns>
    [HttpGet("{cartKey}")]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> GetCartInfoV2(string cartKey)
    {
        IList<CartItem>? items = await _cartManager.GetCartItemsAsync(cartKey);
        if (items is null)
        {
            return NotFound();
        }

        return Ok(items);
    }

    /// <summary>
    /// Adds an item to the cart identified by <paramref name="cartKey"/>, creating the cart if it doesn't exist yet.
    /// </summary>
    /// <param name="cartKey">The unique key of the cart.</param>
    /// <param name="item">The item to add to the cart.</param>
    /// <returns>201 Created with the added item and a Location header pointing to the cart.</returns>
    [HttpPost("{cartKey}/items")]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> AddItemToCartV2(string cartKey, [FromBody] CartItemDto item)
    {
        await _cartManager.AddItemToCartAsync(cartKey, item.ToModel());

        return CreatedAtAction(nameof(GetCartInfoV2), new { cartKey }, item);
    }
}
