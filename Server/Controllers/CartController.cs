using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
  private readonly ICartService cartService;

  public CartController(ICartService cartService)
  {
    this.cartService = cartService;
  }

  [HttpPost("products")]
  public async Task<ActionResult<ServiceResponse<List<CartProductDto>>>> GetCartItems(List<CartItem> cartItems)
  {
    var items = await cartService.GetCartItems(cartItems);
    return Ok(items);
  }
}
