using CartService.BLL;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Web.Controllers;

[ApiController]
[Route("carts")]
public class CartController(CartManager cartManager) : ControllerBase
{
    private readonly CartManager _cartManager = cartManager;

    [HttpGet("{cartKey:string}")]
    public async Task<IActionResult> GetCartInfo(string cartKey)
    {
        return Ok();
    }

    [HttpPost("{cartKey:string}/items")]
    public async Task<IActionResult> AddItemToCart(string cartKey)
    {
        return Ok();
    }

    [HttpDelete("{cartKey:string}/items/{itemId:int}")]
    public async Task<IActionResult> DeleteItemFromCart(string cartKey, int itemId)
    {
        return Ok();
    }
}
