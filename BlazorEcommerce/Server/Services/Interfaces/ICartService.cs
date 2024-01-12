namespace BlazorEcommerce.Server.Services.Interfaces;

public interface ICartService
{
  Task<ServiceResponse<List<CartProductResponse>>> GetCartProducts(List<CartItem> cartItems);
}
