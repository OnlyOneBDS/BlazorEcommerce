using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Client.Services.Interfaces;

public interface ICartService
{
  event Action OnChange;

  Task AddToCart(CartItem cartItem);
  Task RemoveFromCart(int productId, int productTypeId);
  Task UpdateQuantity(CartProductResponse product);
  Task<List<CartItem>> GetCartItems();
  Task<List<CartProductResponse>> GetCartProducts();
}
