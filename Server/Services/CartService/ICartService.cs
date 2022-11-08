namespace BlazorEcommerce.Server.Services.CartService;

public interface ICartService
{
  Task<ServiceResponse<List<CartProductDto>>> GetCartItems(List<CartItem> cartItems);
}
