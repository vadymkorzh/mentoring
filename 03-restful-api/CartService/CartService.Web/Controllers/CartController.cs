using CartService.BLL;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Web.Controllers;

[ApiController]
[Route("carts")]
public class CartController(CartManager cartManager) : ControllerBase
{
    private readonly CartManager _cartManager = cartManager;
}
